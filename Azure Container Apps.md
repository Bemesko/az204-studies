## Resources
- [Preparing for AZ-204 - Develop Azure compute solutions (1 of 5) | Microsoft Learn](https://learn.microsoft.com/en-us/shows/exam-readiness-zone/preparing-for-az-204-develop-azure-compute-solutions-1-of-5)
- [Implement Azure Container Apps - Training | Microsoft Learn](https://learn.microsoft.com/en-us/training/modules/implement-azure-container-apps/)
- [KEDA | Kubernetes Event-driven Autoscaling](https://keda.sh/)
- [Scalers | KEDA](https://keda.sh/docs/2.13/scalers/)
- [Overview | Dapr Docs](https://docs.dapr.io/concepts/overview/)
- [Dapr arguments and annotations for daprd, CLI, and Kubernetes | Dapr Docs](https://docs.dapr.io/reference/arguments-annotations-overview/)
## Key Questions
- What is Azure Container Apps
- What can I do with Azure Container Apps
- Why should I choose Azure Container Apps over Azure Container Instances
- Why should I choose Azure Container Apps over Azure Kubernetes Service
- Why should I choose Azure Container Apps over Azure Web Apps for my containers?
- What are the different autoscaler configurations that I can configure? When should I use each configuration?
- How is pricing calculated
- What can I do with the built in authentication/authorization?
- What are revisions and how can I create them?
- How to I integrate my secrets into Azure Container Apps?
- What are container apps environments? 
- What is Dapr?
- What are the advantages of using Dapr for microservices?
- What is KEDA?
### My Answers
- What is Azure Container Apps
	- Pretty much a serverless Kubernetes platform managed by Azure with a lot of fluff
- What can I do with Azure Container Apps
	- Mostly what you can do with Kubernetes. Deploy containers, mount secrets, auto scale resources, create canary/bluegreen deployments.
	- Easy integration with Dapr to handle complex microservice stuff with minimal configuration
- Why should I choose Azure Container Apps over Azure Container Instances
	- Azure Container Apps can support more complex container orchestration scenarios
	- More suited for microservices, whereas Azure Container Instances is more geared towards deploying individual containers
- Why should I choose Azure Container Apps over Azure Kubernetes Service
	- Easier to deploy
	- Easier identity management
	- Dapr setup by default and run as a service
	- AKS will almost always be cheaper though
- Why should I choose Azure Container Apps over Azure Web Apps for my containers?
	- Mostly the same reasons you would choose it over Azure Container Instances
	- The focus of containers apps is orchestration of a large amount of containers
- What are the different autoscaler configurations that I can configure? When should I use each configuration?
	- HTTP traffic
	- Event-driven processing
	- CPU or memory load
	- Any other KEDA supported metrics
- How is pricing calculated
	- You choose between consumption or preprovisioned pricing
	- Consuption
		- Serverless
		- Way cheaper than preprovisioned
		- Costs are less predictable
	- Preprovisioned
		- You can choose a VM size to run container apps and you will be billed by that VM's usage
- What can I do with the built in authentication/authorization?
	- Pretty easy to integrate with Microsoft, Google, Facebook or event any Oauth2 identity provider
	- No coding required to restrict applications to authorized users
	- Identity providers handle authentication with a sidecar and pass relevant identity info via headers
- What are revisions and how can I create them?
	- Revisions are snapshots of different versions of an app
	- Can be used to update an app and revert changes if necessary
	- Multiple revisions can be activated at a time and the percentage of users routed to them can be specified. This can lead to complex deployment scenarios
- How to I integrate my secrets into Azure Container Apps?
	- You register all secrets to the app itself and reference the secret by name in the configuration
	- No Azure Key Vault integration
- What are container apps environments? 
	- Environments are like the AKS instances for Container Apps. Pretty much the main unit of management of resources
	- Everything in an environment
- What is Dapr?
	- CNCF backed runtime for microservices that handles inter-service communication, secret management, queue messaging, remote procedure calls and other fun stuff
- What are the advantages of using Dapr for microservices?
	- You can do more complex service communication without having to worry about implementing all that by yourself
- What is KEDA?
	- CNCF backed (I think) Kubernetes Event Driven Autoscaling
	- You enable it in Kubernetes and now you can scale your containers based on a bunch of wild stuff, like the amount of blobs in a storage account
## Notes from Learning Path
- Better to follow tutorials using the Azure Portal to create things to avoid mindless copy-pasting
- Container apps are really built for microservices, apparently
- Strong integration with Dapr, which seems like a handler to facilitate communication between services
- Autoscaling options based in KEDA (kubernetes event driven autoscaling)
- Container apps is pretty much a managed Kubernetes with very premade configuration and more limited customization options
- Also they hugely push the serverless model (the cheapest non consumption plan is a D4 machine!)
- Containers can be setup in arm templates, making the ARM templates look a whole lot like Kubernetes manifests or docker compose files, but using JSON
- Container apps limits containers to use the linux/amd64 architecture
### Authentication
- Federated third party authentication providers
- You can add multiple auth providers to the same app and allow users to choose between them
- Relevant identity information is passed in headers to the application itself to handle
- When authentication is enabled, container apps create a sidecar for each app to handle everything
- Authentication flows can be done without code or by using the auth provider SDK:
	- Without SDK (server flow): web apps in which the user can login on their own
	- With SDK (client flow): browser-less apps that don't present a sign in page
### Revisions
- Immutable snapshots of a particular version of the app (like commits)
- Multiple revisions can be enabled at the same time and this allows for complex deployment strategies (routing some of the users to a new revision)
- Each revision is identified by a suffix which can be specified when creating the new revision
### Secrets
- Secrets are created and scoped to a specific app
- all app secrets are accessible to every app revision
	- When deleting a secret it is recommended to create a new revision that doesn't use it and disable previous revisions
- Secrets have a separate lifecycle from apps, so changing them doesn't create a new revision for the app
- Secrets can be referenced in configuration files by using the secretRef and the name of the secret
- Container apps DOESN'T integrate with [Azure Key Vault](Azure%20Key%20Vault). Which is weird. You can use it if you enable managed identity and get secrets via the SDK but we both know we're not gonna do that
### Dapr
![](media/Pasted%20image%2020240126091141.png)
- Dapr is a bunch of different components that can be enabled for different apps and help them communicate with each other without much hassle. It's also backed by the CNCF
- Dapr is enabled through different components that handle different Dapr APIs. Each component can be set up to be enabled for specific apps. Dapr creates a sidecar in each app that will handle that particular component.
![](media/Pasted%20image%2020240126091547.png)

---
## Notes From Exam Prep
- More functionality than [Azure Container Instances](Azure%20Container%20Instances)
- Simpler to implement than [[Azure Kubernetes Service]]
![](media/Pasted%20image%2020240126074803.png)

- Azure container apps have an advantage over ACI because they can dynamically scale:
	- Be familiar with the different configuration options
![](media/Pasted%20image%2020240126074809.png)