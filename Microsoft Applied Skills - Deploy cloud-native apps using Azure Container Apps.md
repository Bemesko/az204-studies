## Sources
### Getting Started
- [Cloud-Native Applications | Microsoft Azure](https://azure.microsoft.com/en-us/solutions/cloud-native-apps/)
- [Introduction to cloud-native applications - .NET | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/architecture/cloud-native/introduction)
- [Comparing Container Apps with other Azure container options | Microsoft Learn](https://learn.microsoft.com/en-us/azure/container-apps/compare-options)
- [Azure Containers—Services and Management | Microsoft Azure](https://azure.microsoft.com/en-us/products/category/containers/)
- [Deploy microservices with Azure Container Apps - Azure Architecture Center | Microsoft Learn](https://learn.microsoft.com/en-us/azure/architecture/example-scenario/serverless/microservices-with-container-apps)
- [mspnp/container-apps-fabrikam-dronedelivery (github.com)](https://github.com/mspnp/container-apps-fabrikam-dronedelivery#expected-results)
### Azure Container Registry
- [Managed container registries - Azure Container Registry | Microsoft Learn](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-intro)
- [Quickstart - Create registry in portal - Azure Container Registry | Microsoft Learn](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-get-started-portal?tabs=azure-cli)
- [Registry service tiers and features - Azure Container Registry | Microsoft Learn](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-skus)
- [Push & pull container image - Azure Container Registry | Microsoft Learn](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-get-started-docker-cli?tabs=azure-cli)
- [Import container images - Azure Container Registry | Microsoft Learn](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-import-images?tabs=azure-cli)
- [Tutorial - Quick container image build - Azure Container Registry | Microsoft Learn](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-tutorial-quick-task)
- [Reference documentation | Docker Docs](https://docs.docker.com/reference/)
- [Authenticate with managed identity - Azure Container Registry | Microsoft Learn](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-authentication-managed-identity?tabs=azure-cli)
- [Manage user-assigned managed identities - Managed identities for Azure resources | Microsoft Learn](https://learn.microsoft.com/en-us/entra/identity/managed-identities-azure-resources/how-manage-user-assigned-managed-identities?pivots=identity-mi-methods-azp)
- [Managed identities in Azure Container Apps | Microsoft Learn](https://learn.microsoft.com/en-us/azure/container-apps/managed-identity?tabs=portal%2Cdotnet)
- [Registry roles and permissions - Azure Container Registry | Microsoft Learn](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-roles?tabs=azure-cli)
- [Steps to assign an Azure role - Azure RBAC | Microsoft Learn](https://learn.microsoft.com/en-us/azure/role-based-access-control/role-assignments-steps)
- [Assign Azure roles using the Azure portal - Azure RBAC | Microsoft Learn](https://learn.microsoft.com/en-us/azure/role-based-access-control/role-assignments-portal?tabs=delegate-condition)
- [Set up private endpoint with private link - Azure Container Registry | Microsoft Learn](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-private-link)
- [Quickstart: Use the Azure portal to create a virtual network - Azure Virtual Network | Microsoft Learn](https://learn.microsoft.com/en-us/azure/virtual-network/quick-create-portal)
### Azure Container Apps
- [Containers in Azure Container Apps | Microsoft Learn](https://learn.microsoft.com/en-us/azure/container-apps/containers)
- [Integrate a virtual network with an external Azure Container Apps environment | Microsoft Learn](https://learn.microsoft.com/en-us/azure/container-apps/vnet-custom?tabs=bash%2Cazure-cli&pivots=azure-portal)
- [Managed identities in Azure Container Apps | Microsoft Learn](https://learn.microsoft.com/en-us/azure/container-apps/managed-identity?tabs=portal%2Cdotnet)
- [Azure Container Apps image pull from Azure Container Registry with managed identity | Microsoft Learn](https://learn.microsoft.com/en-us/azure/container-apps/managed-identity-image-pull?tabs=azure-cli&pivots=azure-portal)
- [Ingress in Azure Container Apps | Microsoft Learn](https://learn.microsoft.com/en-us/azure/container-apps/ingress-overview)
- [Manage secrets in Azure Container Apps | Microsoft Learn](https://learn.microsoft.com/en-us/azure/container-apps/manage-secrets?tabs=azure-portal)
- [Use storage mounts in Azure Container Apps | Microsoft Learn](https://learn.microsoft.com/en-us/azure/container-apps/storage-mounts?pivots=azure-cli)
- [Connect to services in Azure Container Apps (preview) | Microsoft Learn](https://learn.microsoft.com/en-us/azure/container-apps/services)
- [Connect a container app to a cloud service with Service Connector | Microsoft Learn](https://learn.microsoft.com/en-us/azure/container-apps/service-connector?tabs=azure-portal)
- [Quickstart: Deploy your first container app using the Azure portal | Microsoft Learn](https://learn.microsoft.com/en-us/azure/container-apps/quickstart-portal)
- [Deploy Azure Container Apps with the az containerapp up command | Microsoft Learn](https://learn.microsoft.com/en-us/azure/container-apps/containerapp-up)

---
## Getting Started
![](media/Pasted%20image%2020240128111413.png)
*Figure 1: 5 pillars of cloud infrastructure for cloud native applications*
- pets vs. cattle
### Deployment Options for containers
- container apps: kubernetes style applications but without the overhead of managing kubernetes itself
- web apps: great for web apps that don't require too many containers to run
- container instances: less opinionated building block than container apps. one container instance serves 1 container. usually interacted with via other services like aks
- kubernetes service: azure managed kubernetes infrastructure, exposing all kubernetes apis
- azure functions: functions as a service; like container apps functions run based on events; can support containers
- spring apps: ideal for spring boot or spring cloud applications
- red hat openshift: openshift environment managed by azure
### Azure Container Apps
![](media/Pasted%20image%2020240128112608.png)
- single revision mode
![](media/Pasted%20image%2020240128113025.png)

## Configuring Azure Container Registry
![](media/Pasted%20image%2020240128120040.png)
*Figure: Roles for ACR*
## Configuring Azure Container Apps
![](media/Pasted%20image%2020240128125131.png)

![](media/Pasted%20image%2020240128125335.png)
*Love the passive-agressiveness in this*

- job execution as well as regular apps
- sidecars vs. init containers
	- both access resources from the main container
	- sidecars have the same lifecycle as the main container and are tightly coupled
	- init containers run usually to setup the environment for the main container; the main container only runs when all its init containers finish without errors (exit code 0)
	- for loosely coupled containers it is best to create multiple container apps instead of putting everything in a single one

![](media/Pasted%20image%2020240128130834.png)
*"We are definitely better than our competitor, trust us!" (rate limit in docker hub is 100 pulls per 6 hours per ip address*
### Managed Identities
- limitations
	- init containers can't acccess managed identities
	- for private container registries you still need to pass a connection string using the app's secrets
### Ingresses
![](media/Pasted%20image%2020240128132527.png)
- external vs internal
	- external ingresses are publicly available
	- internal ingresses aren't publicly available and can use their own managed vnet or a custom one
- protocol types
	- http
	- tcp (only for custom vnets)
### Secrets
- Key vault integration, apparently
	- Container app needs a managed identity with the "Get" permission on the key vault
	- You set this up with an app secret that makes a reference to the key vault's secret
### Storage
- Container file system
	- Local storage available only to that container
	- is deleted once the container stops or restarts
- Ephemeral
	- Files on the replica that are acessible to the container and sidecards/init containers
	- Equivalent to emptyDir in Kubernetes
	- Not persistent, once a new replica is created this is deleted
- Azure Files
	- Can be mounted as volumes to multiple containers at once
	- Things are persistent
	- Everyone has access to everything in the file share
### Service Connector
- key components of a service connection
	- Source: Usually a compute service
	- Target: Usually a complementary service such as a database or cache storage
	- Client Type: How the connection will be handled (environment variables, connection strings)
	- Authentication Method: Managed identity, connection string or service principal