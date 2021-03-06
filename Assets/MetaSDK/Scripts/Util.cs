﻿using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Timers;
using System.Threading.Tasks;

using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.X509;

namespace MetaSDK.Tools.Util
{
    public static class Util
    {
        public static AsymmetricCipherKeyPair pairKey;

        public static string MakeSessionID()
        {
            string text = "";
            string possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            Random rand = new Random();
            for (int i = 0; i < 8; i++)
            {
                text += possible[rand.Next() % possible.Length];
            }

            Debug.WriteLine("Util.MakeSessionID: " + text);
            return text;
        }

        public static void GetRSAKey(out string pubKey, out string privKey)
        {
            // Generate key pair(public, private)
            RsaKeyPairGenerator generator = new RsaKeyPairGenerator();
            generator.Init(new KeyGenerationParameters(new SecureRandom(), 2048));
            pairKey = generator.GenerateKeyPair();

            // Inject public key from key pair
            SubjectPublicKeyInfo pubKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(pairKey.Public);
            byte[] serializedPubBytes = pubKeyInfo.ToAsn1Object().GetDerEncoded();
            pubKey = Convert.ToBase64String(serializedPubBytes);

            // Inject private key from key pair
            PrivateKeyInfo privKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(pairKey.Private);
            byte[] serializedPrivBytes = privKeyInfo.ToAsn1Object().GetDerEncoded();
            privKey = Convert.ToBase64String(serializedPrivBytes);
        }
    }
}
