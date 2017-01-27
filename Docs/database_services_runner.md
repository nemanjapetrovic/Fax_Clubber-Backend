# ```Clubber``` **clubber.co**
****
# Script for starting the database services

###```Just create .bat file with this code```
### ```Use only if you host on Windows machines```

@echo off

::redis-cli is a redis command line client
cd "C:\NoSQL\Redis\"
start redis-cli.exe

::neo4j app for starting the server
cd "C:\NoSQL\Neo4j CE 3.1.0\bin\"
start neo4j-ce.exe

::mongo db command line client
cd "C:\NoSQL\MongoDB\Server\3.4\bin\"
start mongo.exe

::redis database gui manager and client
::cd ""C:\Program Files (x86)\RedisDesktopManager\"
::start rdm.exe

exit