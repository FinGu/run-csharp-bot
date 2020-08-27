using jw;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace csharp_discord_bot {
    class dotnetfiddle {
		private string api_url = "https://dotnetfiddle.net/api/fiddles";

		private static readonly WebClient client = new WebClient();

        #region structure
        [DataContract]
		public class run_stats_view_model {
			[DataMember]
			public string RunAt { get; set; }
			[DataMember]
			public string CompileTime { get; set; }
			[DataMember]
			public string ExecuteTime { get; set; }
			[DataMember]
			public string MemoryUsage { get; set; }
			[DataMember]
			public string CpuUsage { get; set; }
		}

		[DataContract]
		public class dotnetfiddle_response {
			[DataMember]
			public string ConsoleOutput { get; set; }
			[DataMember]
			public run_stats_view_model Stats { get; set; }
			[DataMember]
			public string WebPageHtmlOutput { get; set; }
			[DataMember]
			public bool IsConsoleInputRequested { get; set; }
			[DataMember]
			public bool HasErrors { get; set; }
			[DataMember]
			public bool HasCompilationErrors { get; set; }
		}
		#endregion

		public dotnetfiddle_response execute_code(string code) {
			var values = new NameValueCollection {
				["Compiler"] = "1", //csharp45, 2 = roslyn
				["Language"] = "1", //csharp
				["ProjectType"] = "1", //console
				["CodeBlock"] = code
			};

			var raw_response = client.UploadValues($"{api_url}/execute", values);

			var response = Encoding.Default.GetString(raw_response);

			var decoded_response = response_decoder.string_to_generic<dotnetfiddle_response>(response);

			return decoded_response;
        }

		private json_wrapper response_decoder = new json_wrapper(new dotnetfiddle_response());
	}
}
