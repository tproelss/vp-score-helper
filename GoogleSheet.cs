using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VPScoreHelper
{
    class GoogleSheet
    {
        const int __maxRows = 200;   // how many rows to read from ADMIN sheet
        private string[] _scopes = { SheetsService.Scope.Spreadsheets };
        private string _applicationName = "VPScoreHelper";

        const string __nameCol = "B";
        const string __scoreCol = "C";
        const int __scoreStartRow = 2;

        private string _spreadsheetId = "";
        private SheetsService _service;

        private List<ScoreEntry> _scoreEntries = null;

        public List<ScoreEntry> ScoreEntries
        {
            get
            {
                return this._scoreEntries;
            }
        }

        private bool _isAttached = false;

        public bool IsAttached
        {
            get { return this._isAttached; }
        }

        public string Title { get; set; }

        public static string CredentialsFile
        {
            get
            {
                if (string.IsNullOrEmpty(__credentialsFile))
                    __credentialsFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "VPScoreHelper.json");

                return __credentialsFile;
            }
        }

        private static string __credentialsFile = null;

        public GoogleSheet()
        {            
        }

        public delegate void EventHandler(object sender, EventArgs e);
        public event EventHandler Attached;

        public void Attach(string spreadsheetId)
        {
            this._spreadsheetId = spreadsheetId;
            Log.Instance.WriteLine(LogLevel.Info, $"Attaching spreadsheet {spreadsheetId}");

            GoogleCredential credential;
            
            using (var stream = new FileStream(CredentialsFile, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(_scopes);
            }
            
            this._service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _applicationName
            });
            
            var r = _service.Spreadsheets.Get(spreadsheetId);
            Spreadsheet spreadsheet = r.Execute();

            this.Title = spreadsheet.Properties.Title;
            Log.Instance.WriteLine(LogLevel.Info, $"Title: {spreadsheet.Properties.Title}");
            
            RefreshLocalScoreEntries();                       

            this._isAttached = true;

            EventHandler temp = this.Attached;
            if (temp != null)
            {
                this.Attached(this, null);
            }            
        }        

        private void RefreshLocalScoreEntries()
        {
            this._scoreEntries = GetScoreEntries();
        }

        private List<ScoreEntry> GetScoreEntries()
        {
            Log.Instance.WriteLine(LogLevel.Info, $"Try to load score entries from spread sheet");

            var result = new List<ScoreEntry>();

            String range = $"'ADMIN'!{__nameCol}{__scoreStartRow}:{__scoreCol}{__maxRows}";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    _service.Spreadsheets.Values.Get(_spreadsheetId, range);

            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            if (values == null && values.Count <= 0)
            {
                Log.Instance.WriteLine(LogLevel.Info, $"Hm, no scores found");
                return result;
            }

            var currentRow = __scoreStartRow;

            foreach (var row in values)
            {
                var name = row[0] as string;
                if (string.IsNullOrEmpty(name))
                    break;

                if (name.ToLower().StartsWith("spare"))
                    break;

                var newItem = new ScoreEntry();
                newItem.Name = name;

                if (row.Count > 1)
                {
                    newItem.Score = ConversionHelper.GetScoreFromString((string)row[1]);
                }

                newItem.NameCell = $"{__nameCol}{currentRow}";
                newItem.ScoreCell = $"{__scoreCol}{currentRow}";

                result.Add(newItem);

                currentRow++;
            }
            
            //result.ForEach(s => Log.Instance.WriteLine(LogLevel.Verbose, $"{s.Name} - {s.Score}"));
            Log.Instance.WriteLine(LogLevel.Info, $"Found {result.Count} entries");

            if (result.Count > 0)
                Log.Instance.WriteLine(LogLevel.Info, $"Lets go, buddy!");

            return result;
        }

        private string GetCellValue(string cell)
        {
            String range = $"'ADMIN'!{cell}";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    _service.Spreadsheets.Values.Get(_spreadsheetId, range);

            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;

            if (values == null && values.Count <= 0)
                return null;

            if (values[0].Count <= 0)
                return null;

            if (values[0][0] == null)
                return null;

            return values[0][0] as string;
        }

        public ScoreEntry GetScoreEntryByName(string name)
        {
            if (_scoreEntries == null)
                return null;

            return _scoreEntries.Where(x => x.Name.Trim().ToLower() == name.Trim().ToLower()).FirstOrDefault();
        }

        public bool WriteToSheet(ScoreEntry scoreEntry)
        {
            try
            {
                // validate name
                var currentName = GetCellValue(scoreEntry.NameCell);
                if (currentName != scoreEntry.Name)
                    throw new ArgumentException($"Sheet contains unexpected name '{currentName}' at {scoreEntry.NameCell}");

                ValueRange valueRange = new ValueRange();
                valueRange.MajorDimension = "COLUMNS";

                var oblist = new List<object>() { $"{ConversionHelper.GetStringFromScore(scoreEntry.Score)}" };
                valueRange.Values = new List<IList<object>> { oblist };

                var range = $"ADMIN!{scoreEntry.ScoreCell}";

                SpreadsheetsResource.ValuesResource.UpdateRequest update = _service.Spreadsheets.Values.Update(valueRange, this._spreadsheetId, range);                
                update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
                
                UpdateValuesResponse result = update.Execute();

                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.WriteLine(LogLevel.Error, $"Exception {ex.Message}\n{ex.StackTrace}");
                return false;
            }
        }        
    }
}
