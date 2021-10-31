using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Trsys.Ea.Abstractions;

namespace Trsys.Ea.Infrastructure
{
    public class EaSessionTokenParser : IEaSessionTokenParser
    {
        class Session
        {
            public string Id { get; set; }
            public string Key { get; set; }
            public string KeyType { get; set; }
        }

        private readonly JwtDecoder decoder;

        public EaSessionTokenParser()
        {
            var serializer = new JsonNetSerializer();
            var provider = new UtcDateTimeProvider();
            var validator = new JwtValidator(serializer, provider);
            var urlEncoder = new JwtBase64UrlEncoder();
            var algorithm = new HMACSHA256Algorithm();
            decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
        }

        public EaSession ExtractToken(string token)
        {
            try
            {
                var session = decoder.DecodeToObject<Session>(token, "s3cr3t", verify: true);
                return new EaSession(session.Id, session.Key, session.KeyType, token);
            }
            catch
            {
                throw new EaSessionTokenInvalidException();
            }
        }
    }
}