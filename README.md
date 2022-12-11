# simple-SSHClient.cs

C#で実装した簡単なSSHクライアントプログラム。  

## 使い方

```shell
./simple-SSHClient.exe /h ホスト名 /p ポート番号 /u ユーザ名 /k 秘密鍵ファイルパス /passphrase パスフレーズ
```

「ls」コマンドの実行結果が出力されます。  

## 注意事項

> **Warning**  
> 使用できる鍵の形式が限定されています。  
> pem方式での動作確認はでき、OPEN SSH形式の鍵は正常に動作しません。  
