# ```Clubber``` **clubber.co**
****
# ```Installing the databases```
------

## Initialization
- Open C: partition
- Create **NoSQL** folder

## MongoDB
- Install **mongodb-win32-x86_64-2008plus-ssl-3.4.1-signed.msi**

## RedisDB
- Install **Redis-x64-3.2.100.msi**

## Neo4jDB
- Install **neo4j-community_windows-x64_3_1_0.exe**

# ```Initializing the databases```
------

## MongoDB

### Initialization
- Open MongoDB installation folder
- Run in ```CMD```: **mongod --port 27017 --dbpath C:\MongoDB\data\db**

### Creating the database and collections
- Start mongo.exe in ```CMD```
- Run these commands:
	- **use clubbertest**
	- **db.createCollection("user")**
	- **db.createCollection("manager")**
	- **db.createCollection("club")**
	- **db.createCollection("event")**

## RedisDB

### Initialization
- Open .config file
- Change "The working directory." to the directory where your database dump will be stored
- Change a name of file that will store the database

### Starting the server service
- Open Redis installation folder
- To create a redis windows service (if it's not created already) run in ```CMD```: **redis-server --service-install redis.windows.conf --loglevel verbose**
- To start a redis server with log run in ```CMD```: **redis-server redis.windows.conf --loglevel verbose**

## Neo4jDB

### Initialization
- Run neo4j client
- Change database location
- Start the server
- Change username and password for database authorization

# ```Setting up the Azure platform```
------

# ```Installing the WebAPI```
------