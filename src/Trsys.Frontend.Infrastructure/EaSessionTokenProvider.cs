using System;
using System.Collections.Generic;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;

namespace Trsys.Frontend.Infrastructure
{
    public class EaSessionTokenProvider : IEaSessionTokenProvider
    {
        private readonly JwtEncoder encoder;

        public EaSessionTokenProvider()
        {
            var algorithm = new HMACSHA256Algorithm();
            var serializer = new JsonNetSerializer();
            var urlEncoder = new JwtBase64UrlEncoder();
            encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
        }

        public string GenerateToken(string id, string key, string keyType)
        {
            return encoder.Encode(new Dictionary<string, object>() {
                {"id", id},
                {"key", key},
                {"keyType", keyType},
                {"sessionId", Guid.NewGuid().ToString() },
            }, "s3cr3t");
        }
    }
}