using log4net;
using NHibernate;
using RaioXVegano.Util;
using System;

namespace RaioXVegano.so.BancoDeDados
{
    public static class BancoDeDadosUtil
    {
        public static void Atualizar<T>(T t, ISession _sessao, ILog _log)
        {
            using (var transacao = _sessao.BeginTransaction())
            {
                try
                {
                    _log.Info($"Atualizar... ");
                    _sessao.Merge(t);
                    transacao.Commit();
                    _log.Info("Atualizar... OK");
                }
                catch (Exception e)
                {
                    _log.Error(e);
                    throw e;
                }
                finally
                {
                    transacao.Dispose();
                    _sessao.Flush();
                    _sessao.Close();
                }
            }
        }

        public static int Inserir<T>(T t, ISession _sessao, ILog _log)
        {
            int id;
            using (var transacao = _sessao.BeginTransaction())
            {
                try
                {
                    _log.Info($"Inserir... ");
                    id = (int)_sessao.Save(t);
                    transacao.Commit();
                    _log.Info("Inserir... OK");
                }
                catch (Exception e)
                {
                    _log.Error(e);
                    throw e;
                }
                finally
                {
                    transacao.Dispose();
                    _sessao.Flush();
                    _sessao.Close();
                }
            }

            return id;
        }

        public static T ListarPorId<T>(int id, ISession _sessao, ILog _log)
        {
            _log.Info("ListarPorId...");
            T t = _sessao.Get<T>(id);
            _log.Info($"ListarPorId... OK");

            return t;
        }
    }
}
