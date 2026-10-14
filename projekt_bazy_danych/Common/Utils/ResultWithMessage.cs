namespace projekt_bazy_danych.Common.Utils
{
    public class ResultWithMessage
    {
        #region Properties
        public bool IsValid { get; set; }

        public string? Message { get; set; }
        #endregion

        #region Constructors
        public ResultWithMessage(bool IsValid)
        {
            this.IsValid = IsValid;
        }

        public ResultWithMessage(bool IsValid, string Message)
        {
            this.IsValid = IsValid;
            this.Message = Message;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return Message ?? "Wiadomosc resultu jest pusta";
        }
        #endregion
    }
}
