using Entites.Contracts;

namespace DB.Facade.Translators
{
    internal class UserTranslator : ITranslator<Org.Feeder.FeederDb.User, Entites.User>
    {
        #region ITranslator<User,User> Members

        public Entites.User Translate(Org.Feeder.FeederDb.User source)
        {
            if (source == null)
            {
                return null;
            }

            return new Entites.User()
            {
                Email = source.Email,
                ImageUrl = source.ImageUrl,
                Name = source.Name,
                UserId = new UserId(source.UserId)
            };
        }

        #endregion
    }
}
