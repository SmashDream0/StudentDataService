using StudentDataService.Contracts.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientTest
{
    class Client
    {
        public Client(string baseUrl)
        {
            BaseUrl = baseUrl;
            JsonSerializerSettings = CreateSerializerSettings();
            _httpClient = new System.Net.Http.HttpClient();
        }

        private Newtonsoft.Json.JsonSerializerSettings CreateSerializerSettings()
        {
            var settings = new Newtonsoft.Json.JsonSerializerSettings();
            return settings;
        }

        /// <summary>
        /// Базовый адрес сервиса квот
        /// </summary>
        public string BaseUrl
        { get; set; }

        private System.Net.Http.HttpClient _httpClient;
        protected Newtonsoft.Json.JsonSerializerSettings JsonSerializerSettings { get; private set; }

        public async System.Threading.Tasks.Task<int> StudentInsert(StudentCRUID body, System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/Student/Insert?");
            var client_ = _httpClient;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    var content_ = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(body, JsonSerializerSettings));
                    try
                    {
                        client_.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJyb2xlIjoiYWRtaW4iLCJuYmYiOjE1OTg0MjE2NjQsImV4cCI6MTU5OTAyNjQ2NCwiaWF0IjoxNTk4NDIxNjY0fQ.wJEE3AVTu_t57M8RnQ8hjMDPWw6zQxdiEbc1PELlVlo");
                    }
                    catch (Exception ex)
                    { }
                    request_.Content = content_;
                    request_.Method = new System.Net.Http.HttpMethod("POST");
                    content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    Console.WriteLine("Отправляю запрос");
                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        Console.WriteLine("Получаю ответ");

                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }
                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<int>(response_, headers_).ConfigureAwait(false);
                            Console.WriteLine("Возвращаю ответ");
                            return objectResponse_.Object;
                        }
                        else if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            Console.WriteLine("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").");
                        }

                        return -1;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        private System.Net.Http.HttpRequestMessage Clone(System.Net.Http.HttpRequestMessage request)
        {
            var result = new System.Net.Http.HttpRequestMessage(request.Method, request.RequestUri);

            result.Content = request.Content;
            result.Version = request.Version;

            foreach (KeyValuePair<string, object> prop in request.Properties)
            { result.Properties.Add(prop); }

            foreach (KeyValuePair<string, IEnumerable<string>> header in request.Headers)
            { result.Headers.TryAddWithoutValidation(header.Key, header.Value); }

            return result;
        }

        protected struct ObjectResponseResult<T>
        {
            public ObjectResponseResult(T responseObject, string responseText)
            {
                _object = responseObject;
                _text = responseText;
            }

            private T _object;
            private string _text;

            public T Object { get { return _object; } }

            public string Text { get { return _text; } }
        }

        public bool ReadResponseAsString { get; set; }

        protected virtual async System.Threading.Tasks.Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(System.Net.Http.HttpResponseMessage response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers)
        {
            if (response == null || response.Content == null)
            {
                return new ObjectResponseResult<T>(default(T), string.Empty);
            }

            if (ReadResponseAsString)
            {
                var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    var typedBody = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseText, JsonSerializerSettings);
                    return new ObjectResponseResult<T>(typedBody, responseText);
                }
                catch (Newtonsoft.Json.JsonException exception)
                {
                    var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
                    throw new Exception(message, exception);
                }
            }
            else
            {
                try
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (var streamReader = new System.IO.StreamReader(responseStream))
                    using (var jsonTextReader = new Newtonsoft.Json.JsonTextReader(streamReader))
                    {
                        var serializer = Newtonsoft.Json.JsonSerializer.Create(JsonSerializerSettings);
                        var typedBody = serializer.Deserialize<T>(jsonTextReader);
                        return new ObjectResponseResult<T>(typedBody, string.Empty);
                    }
                }
                catch (Newtonsoft.Json.JsonException exception)
                {
                    var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
                    throw new Exception(message, exception);
                }
            }
        }

        private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value is System.Enum)
            {
                string name = System.Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                    if (field != null)
                    {
                        var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute))
                            as System.Runtime.Serialization.EnumMemberAttribute;
                        if (attribute != null)
                        {
                            return attribute.Value != null ? attribute.Value : name;
                        }
                    }

                    return System.Convert.ToString(System.Convert.ChangeType(value, System.Enum.GetUnderlyingType(value.GetType()), cultureInfo));
                }
            }
            else if (value is bool)
            {
                return System.Convert.ToString(value, cultureInfo).ToLowerInvariant();
            }
            else if (value is byte[])
            {
                return System.Convert.ToBase64String((byte[])value);
            }
            else if (value != null && typeof(System.Collections.IEnumerable).IsAssignableFrom(value.GetType()))
            {
                var array = System.Linq.Enumerable.OfType<object>((System.Collections.IEnumerable)value);
                return string.Join(",", System.Linq.Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
            }

            return System.Convert.ToString(value, cultureInfo);
        }
    }
}
