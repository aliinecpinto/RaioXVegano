using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using RaioXVegano.Util;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RaioXVegano.App.Helpers
{
    public static class ConfigurationManager
    {
        public static void Configure()
        {
            Criptografia();

            Preferences.Set(Consts.RUN_LOCAL, Consts.FALSE);
            //Preferences.Set(Consts.URL, "https://192.168.0.150:44304/api/");
            Preferences.Set(Consts.URL, "https://192.168.43.112:44304/api/");
            Preferences.Set(Consts.SESSAO_CHAVE_USUARIO_LOGADO, GeraChaveUsuario());

            AplicacaoUtil.Ambiente = Consts.AMBIENTE_DEV;

            ConfigureNLog();
        }

        private static void Criptografia()
        {
            string chave = SecureStorage.GetAsync(Consts.SESSAO_CHAVE_CRIPTOGRAFIA).Result;
            string IV = SecureStorage.GetAsync(Consts.SESSAO_IV_CRIPTOGRAFIA).Result;

            if (string.IsNullOrEmpty(chave) && string.IsNullOrEmpty(IV))
            {
                CriptografiaUtil.GerarChaves();

                SecureStorage.SetAsync(Consts.SESSAO_CHAVE_CRIPTOGRAFIA, CriptografiaUtil.AESKey);
                SecureStorage.SetAsync(Consts.SESSAO_IV_CRIPTOGRAFIA, CriptografiaUtil.AESIV);
            }
            else 
            {
                CriptografiaUtil.PreencherComChavesExistentes(chave, IV);
            }
        }

        private static string GeraChaveUsuario()
        {
            Random numAleatorio = new Random();
            return $"USUARIO_{numAleatorio.Next(1, int.MaxValue)}";
        }

        private static void ConfigureNLog()
        {
            string folder = DependencyService.Get<IExternalStorage>().GetExternalStorage();

            FileTarget logFile = new FileTarget("logfile")
            {
                ArchiveAboveSize = 10000000,
                ArchiveDateFormat = "yyyyMMdd",
                ArchiveEvery = FileArchivePeriod.Day,
                ArchiveNumbering = ArchiveNumberingMode.DateAndSequence,
                FileName = $"{folder}/Logs/log.log",
                MaxArchiveFiles = 10,
                Layout = new SimpleLayout(@"${date:format=yyyy-MM-dd HH\:mm\:ss.fff} [${threadid}] ${level} ${logger} - ${message}")
            };

            LoggingConfiguration configuration = new LoggingConfiguration();
            configuration.AddRule(LogLevel.Info, LogLevel.Info, logFile);

            LogManager.Configuration = configuration;
            LogManager.ReconfigExistingLoggers();
        }
    }
}
