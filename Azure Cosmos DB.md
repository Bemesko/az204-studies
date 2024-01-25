## Sources
- [Preparing for AZ-204 - Develop for Azure storage (segment 2 of 5) | Microsoft Learn](https://learn.microsoft.com/en-us/shows/exam-readiness-zone/preparing-for-az-204-develop-for-azure-storage-segment-2-of-5)
- [AZ-204: Develop solutions that use Azure Cosmos DB - Training | Microsoft Learn](https://learn.microsoft.com/en-us/training/paths/az-204-develop-solutions-that-use-azure-cosmos-db/)
- [Escolher nível certo de consistência - Training | Microsoft Learn](https://learn.microsoft.com/pt-br/training/modules/explore-azure-cosmos-db/5-choose-cosmos-db-consistency-level)
- [Opções de nível de coerência - Azure Cosmos DB | Microsoft Learn](https://learn.microsoft.com/pt-br/azure/cosmos-db/consistency-levels)
- [Use stored procedures, triggers, and UDFs in SDKs - Azure Cosmos DB | Microsoft Learn](https://learn.microsoft.com/en-us/azure/cosmos-db/nosql/how-to-use-stored-procedures-triggers-udfs?tabs=dotnet-sdk-v2)
## What I need to know
- What is Cosmos DB?
- When should I use cosmos db?
- What is the resource hierarchy?
- What are cosmos db accounts? When should I use it?
- What are cosmos db databases? When should I use it?
- What are cosmos containers? When should I use it?
- What are cosmos items? When should I use it?
- How can I use the sdk to interact with containers
- How can I use the sdk to interact with items?
- What are the different cosmos db apis?
- When should I use each cosmos db api?
- What are request units
- What are consistency levels?
- When should I choose each consistency level?
- What are change feed notifications?
- How do I handle deletes from a change feed?
- How can I integrate an Azure Function with change feed
- How can I use the Change Feed processor?
- How can I process changes in parallel?
### My answers
- What is Cosmos DB?
	- Microsoft's NoSQL geo-distributed database with high availability and low latency built in
- When should I use cosmos db?
	- You need a globally distributed database
	- You have a current database but need to migrate to a more powerful/available option
	- High availability and low latency for all your users is your priority
- What is the resource hierarchy?
	- accounts
		- databases
			- containers
				- items
- What are cosmos db accounts? When should I use it?
	- Accounts are the main representation of a cosmos db instance in an azure subscription. They work like storage account in which they are the main point to manage databases. Everything in cosmos db needs to be created inside an account
- What are cosmos db databases? When should I use it?
	- Databases are organizational units for containers. Work like namespaces.
- What are cosmos containers? When should I use it?
	- Containers are the organizational units that hold items. Can work like tables in a relational database. All items belong to a container, but a container doesn't enforce a schema in its items
- What are cosmos items? When should I use it?
	- Items are the main data entities in Cosmos DB. They work like rows in a table.
- How can I use the sdk to interact with containers
	- There is an SDK available for various languages that you can use to interact with Cosmos. You create a cosmos client that is referenced throughout the lifetime of the application that will handle connections and use it to get references to containers (using the CreateIfNotExists methods). It is best practice to call only async functions when dealing with cosmos
- How can I use the sdk to interact with items?
	- Using the sdk you can define items as JSON documents and insert them into the container.
- What are the different cosmos db apis?
	- Cosmos db has a different API for each kind of database. Available APIs are NoSQL (default, Microsoft-maintained), MongoDB (document), PostgreSQL (relational), Cassandra (columnar), Gremlin (graph) and Table (key/value)
- When should I use each cosmos db api?
	- By default you should use NoSQL unless you are migrating an application that uses a different type of database or want to work with that database's open source ecossystem
- What are request units
	- RUs are the main unit in which Cosmos DB costs are calculated.
	- Equivalent to the memory, cpu and iops required to read 1 item of 1kb through its index and partition key
	- Cosmos is billed in RUs/second and has 3 pricing models (preprovisioned, serverless and autoscaling)
- What are consistency levels?
	- The consistency level is a setting that determines how consistent all the instances of a Cosmos DB database should be at all times
	- With less consistency comes higher availability, lower latency and higher throughput
	- Strong: every transaction is replicated to all instances before it is fully committed
	- Bounded Staleness: You can define how far behind (in changes or time) items can be when people read them
	- Session: everyone with the same session token reads the same thing
	- Consistent Prefix: Changes are replicated assynchronously but in the order in which operations were made
	- Eventual: Changes will be replicated out of order after some time
- When should I choose each consistency level?
	- Strong: you need everyone to always be up to date
	- Bounded staleness: You need a lot of consistency, but changes don't always need to be reflected in real time to all users
	- Session: You want applications that use the same data to be consistent between each other
	- Consistent prefix: You need a lot of throughput but the order of operations is important
	- Eventual: you only care for speed and you data doesn't need to be ordered
- What are change feed notifications?
	- A feed of create or update actions that were made to a container in order
- How do I handle deletes from a change feed?
	- Create a property for a deleted flag in items and mark them with a TTL after it is assigned
	- In other words, soft deleting
- How can I integrate an Azure Function with change feed
	- There is a Cosmos DB Azure Function trigger that will execute a function whenever a change is made
- How can I use the Change Feed processor?
	- You need:
		- A monitored container
		- A lease container to keep track of all clients for the change feed notifications (since the notification will be pushed by Cosmos itself)
		- A compute instance to host the client
		- A delegate function that will run whenever a change notification is received
- How can I process changes in parallel?
	- Not sure... Maybe using partition keys 

---
## Random Notes
- Azure gives 1000RU/s for free to each subscription in the pre-provisioned plan
---
## Notes from Microsoft Learn Path
### Overview
- Cosmos' Big Thing: Globally distributed nosql databases that all support reads and writes with very low latency
- Regions can be added or removed without downtime
- 99.999% availability because if one region is unavailable another will answer
- Very low latency because the database closest to the user will respond
### Resource Hierarchy
*Cosmos DB Resource Hierarchy*
![](media/Pasted%20image%2020240124075550.png)
- Accounts
	- Cosmos accounts are pretty much storage accounts but for cosmos
	- When you want new regions you add them to accounts
- Databases
	- Like namespaces that organize containers
	- Organization purposes mainly
- Containers
	- Unit of scalability
	- Containers are like tables in which they are a collection of items
	- Schema-less, so items don't need to have similar properties to be in the same container
	- Globally distributed using partition keys
	- Every item in a container is automatically indexed
	- 2 ways to setup resource provisioning
		- Dedicated: this container will have resources all to itself, backed up by the SLAs
		- Shared: sort of like an elastic pool, all containers using the shared resources will... share the resources
- Items
	- Specific implementation depends on which cosmos API is being used
	- Can be thought as rows for a table
### Consistency Levels
- Each consistency level is a choice between how consistent your databases should be, their cost, latency and throughput
- There are 5 consistency Levels: Strong, Bounded Staleness, Session, Consistent Prefix and Eventual
Some
Bombs
Should
Come
Eventually

- Strong
	- As linear as it can get
	- Everyone always sees the latest version of all the changes
	- No one sees a partial change
	- More latency because everything needs to be replicated before it is committed
- Bounded Staleness
	- Like strong, but updates don't necessarily happen in real time, although their order is preserved
	- You can set how outdaded a read can be using a maximum amount of Time or Versions (changes) that are acceptable to lag behind
	- For a single region account, the minimum value of K and T is 10 write operations or 5 seconds. For multi-region accounts the minimum value of K and T is 100,000 write operations or 300 seconds.
- Session consistency
	- Everyone sharing a session token will see the same data
	- Updates by the session will be guaranteed to be shown
- Consistent Prefix
	- Honors the order of updates
	- Related updates are always replicated together (you can see Av1, Bv1 or Av2 or Bv2 but not Av1,Bv2)
- Eventual Consistency
	- No ordering guarantee whatsoever
	- Replicas eventually converge if there aren't more writes
	- Someone can read values that are older than the values you read before
	- Use case: number of likes in a post

### Cosmos APIs
 NoSQL is the native for CosmosDB and should be the default. Other APIs exist if you already have an application that uses another type of database and you want to migrate to Cosmos

- NoSQL
	- Items are documents
	- Native to Cosmos DB, maintained by Azure and will always receive the newest features first
	- Items can be queried using SQL
- MongoDB
	- Items are also documents, in this case BSON
	- Acts like a mongoDB database
- PostgreSQL
	- Items are rows in a table
	- Data can be stored in a single node or distributed between various nodes
- Cassandra
	- Column database
- Gremlin
	- Graph database
- Table
	- Key/value database
	- Souped up version of an Azure Table Storage, you should migrate to it if Azure Tables doesn't have enough performance
### Request Units
- 1 RU is the CPU, IOPS and memory required to Read an 1KB item using its index
- All operations are measured in RUs
![](media/Pasted%20image%2020240124085644.png)
- There are 3 pricing models for Cosmos
	- Preprovisioned
		- You specify the amount of RUs per second that your application can use
		- You can manually increase or decrease the RUs available by 100 units at a time
		- There isn't downtime to change the amount of RUs provisioned
		- You get billed for the RUs provisioned
		- If more resources than provisioned are necessary, throughput is affected
		- Provisioned RUs can be specified at a container or database level
	- Serverless
		- You pay for what you use
		- Probably the most unpredictable and can cause surprises
	- Autoscale
		- Automatically scales the RUs/s of the database or container based on the usage, without downtime
		- Good for workloads with variable traffic patterns and that requre SLAs on performance and scale

### SDKs
- The NoSQL API has SDKs for various languages
- I'll practice with the Python one
### Stored Procedures
- Stored procedures are written in javascript and need to be registered in the Cosmos DB Database
- All parameters are passed with type string, so they need to be parsed into the correct type by the procedure
- All Cosmos collection functions are asynchronous and return a boolean showing if the operation was successful or not
- All functions have a limited amount of time in which they can run (bounded execution)
- Stored procedures support transactions:
![](media/Pasted%20image%2020240125083319.png)
### Triggers and Functions
- Pretriggers
	- Run before an operation is made
	- Can access objects in the request object
- Posttriggers
	- Run after an operation is made
	- Part of the same transaction as the operation it's after. If the post trigger fails the whole thing is rolled back
	- Can access objects in the response object
- User Defined Functions
	- Can be used in other registered procedures or triggers
### Change Feeds
- A change feed is a way of sending all changes that were made to a cosmos db container, preserving their order
- Only create and update operations are sent in the change feed
	- To track deletes they literally suggest creating a "deleted" flag and setting a ttl on the item after it is set to be deleted
- Ways to read the change feed
	- Change processor in the .net or java sdk
		- Needs 2 containers (monitored and lease container), one to be tracked and one to track the state of clients (since cosmos will push the change notifications to them)
		- Also requires a compute instance to host the change feed client and the delegate, which is the code that will be run by the client whenever a change is made
	- Cosmos DB Azure Functions trigger, which uses the change processor in the background
- Clients can attach to the change feed using a push or pull model
	- push
		- cosmos itself sends the change notifications to all clients
		- recommended way to to things, should be the default if you don't have a good reason to use the other model
	- pull
		- clients need to explicitly pull all changes from cosmos
		- good reasons to use
			- read changes only for a particular partition key
			- control when the clients receive notifications
			- get all notifications only once (for migration purposes for example)
---
## Notes from Exam Prep
- resource hierarchy in cosmos database
	- accounts
	- databases
	- containers
	- items
- use the sdk to perform operations in containers and items
- how to configure request units
![](media/Pasted%20image%2020240124073527.png)

- consistency levels
	- strong: all writes are replicated to all instances before commiting
	- bounded staleness: like strong, but you can configure how much time a document can be outdated (stale)
	- session: everyone with the same session token can see the same operations
	- consistent prefix: all updates will show up in the correct order, eventually
	- eventual: less consistent, primary database doesn't need to wait for replicas to commit
![](media/Pasted%20image%2020240124073642.png)
![](media/Pasted%20image%2020240124073718.png)
- Change feed notifications
	- cosmos db can stream change notifications to a change feed consumer
![](media/Pasted%20image%2020240124074114.png)
## Practice Questions

![](media/Pasted%20image%2020240123085928.png)
![](media/Pasted%20image%2020240123090011.png)
![](media/Pasted%20image%2020240124090144.png)
