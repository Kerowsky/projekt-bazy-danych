namespace projekt_bazy_danych.Common.Utils
{
    public sealed class ResultObjectWithMessage<T> : ResultWithMessage where T : class
    {
        #region Properties
        public T Object { get; set; }
        #endregion

        #region Constructors
        public ResultObjectWithMessage(bool isValid, string message, T Object) : base (isValid, message)
        {
            this.Object = Object;
        }
        #endregion

        #region Methods
        #endregion
    }
}
