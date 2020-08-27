using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace jw {
    public class json_wrapper {
        public static bool is_serializable(Type to_check) =>
            to_check.IsSerializable || to_check.IsDefined(typeof(DataContractAttribute), true);

        public json_wrapper(object obj_to_work_with) {
            current_object = obj_to_work_with;

            var object_type = current_object.GetType();

            serializer = new DataContractJsonSerializer(object_type);

            if (!is_serializable(object_type))
                throw new Exception($"the object {current_object} isn't serializable");
        }

        public string to_json_string() {
            using (var mem_stream = new MemoryStream()) {
                serializer.WriteObject(mem_stream, current_object);

                mem_stream.Position = 0;

                using (var reader = new StreamReader(mem_stream))
                    return reader.ReadToEnd();
            }
        }

        public object string_to_object(string json) {
            var buffer = Encoding.Default.GetBytes(json);

            using (var mem_stream = new MemoryStream(buffer))
                return serializer.ReadObject(mem_stream);
        }

        #region extras
        public dynamic string_to_dynamic(string json) =>
            (dynamic)string_to_object(json);

        public T string_to_generic<T>(string json) =>
            (T)string_to_object(json);

        public dynamic to_json_dynamic() =>
            string_to_object(to_json_string());

        #endregion

        private DataContractJsonSerializer serializer;

        private object current_object;
    }
}