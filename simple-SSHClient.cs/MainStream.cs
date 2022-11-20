using Microsoft.Extensions.Configuration;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

internal static partial class Program
{
  internal static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

  internal static string Host = "";
  internal static int Port = 22;
  internal static string UserName = "";
  internal static string PrivateKey = "";
  internal static string? PassPhrase;

  internal static void MainStream(string[] args)
  {
    var builder = new ConfigurationBuilder();
    builder.AddCommandLine(args);

    var config = builder.Build();

    var tmp_host = config["h"];
    if (string.IsNullOrEmpty(tmp_host))
    {
      logger.Error("ホスト名が指定されていません。");
      return;
    }
    Host = tmp_host;

    var tmp_port = config["p"];
    if (string.IsNullOrEmpty(tmp_port))
    {
      logger.Error("ポート番号が指定されていません。");
      return;
    }
    if (!int.TryParse(tmp_port, out Port))
    {
      logger.Error("ポート番号が不正です。");
      return;
    }

    var tmp_user = config["u"];
    if (string.IsNullOrEmpty(tmp_user))
    {
      logger.Error("ユーザー名が指定されていません。");
      return;
    }
    UserName = tmp_user;

    var tmp_privateKey = config["k"];
    if (string.IsNullOrEmpty(tmp_privateKey))
    {
      logger.Error("秘密鍵が指定されていません。");
      return;
    }
    PrivateKey = tmp_privateKey;

    var client = new SshClient(Host, Port, UserName, new PrivateKeyFile(PrivateKey, PassPhrase));

    logger.Info("接続を開始します。");

    client.Connect();

    logger.Info("接続が完了しました。");
    
    if (!client.IsConnected) throw new AuthenticationException();
    Console.WriteLine(client.CreateCommand("ls").Execute());
    client.Disconnect();
  }
}
