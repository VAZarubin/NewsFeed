namespace DB.Facade.Translators
{
    internal interface ITranslator<in TInputType, out TOutType>
    {
        #region Methods

        TOutType Translate(TInputType source);

        #endregion
    }
}
