## Sources
- [Manage container images in Azure Container Registry - Training | Microsoft Learn](https://learn.microsoft.com/en-us/training/modules/publish-container-image-to-azure-container-registry/)

## Key Questions
- What is Azure Container Registry?
- When should I use container registry?
- Why should I use container registry instead of Docker Hub or other alternatives?
- What are ACR tasks?
- What are quick tasks? When should I use them?
- What are automatically triggered tasks? When should I use them?
- What are the types of automatically triggered tasks and why are they useful?
- What are multi-step tasks? When should I use them?
- What are YAML configuration files used for in ACR?
- What are the different service tiers?
- When should I use each service tier?
- How does pricing work?
- What are supported images and artifacts? When should I use them?
- What else can ACR store besides Docker images?
- What are the limitations of storing images in ACR?
- How do I manage artifacts in an ACR repository
### My Answers

---
## Notes from Learning Path
- You can use either ACR as a registry for existing container deployments or use it along with ACR tasks like a full pipeline runtime
- ACR tasks can rebuild images when their base images are updated
- You can store docker images, helm charts and OCI images
- For some reason ACR is listed in the azure portal as "Container Registries". Searching for "ACR" or "Azure Container Registry" doesn't work
### Service Tiers
- Only model of pricing, making it predictable
- Basic
	- Supports programmatic features of higher tiers (webhooks)
	- Has storage and throughput suitable for lower workloads
- Standard
	- Pretty much the basic plan but with more storage and throughput
- Premium
	- Largest storage and throughput
	- Can use private link to be acessible only in private VNETs
	- Registries can be multi-region
	- Content trust for image signing
### Storage
- encryption at rest (stored things are encrypted when they are stored and only decrypted when they are retrieved)
- regional storage: in case of regional outages, things get replicated to paired azure regions in the same geography (except brazil south and southeast asia).
	- geo replication can be enabled if there is a need for pairing accross geographies
- zone redundancy for storing images
### ACR Tasks
- 3 types of ACR Tasks
- Quick tasks: `az acr build` in your local directory. Basically docker build and docker push in the cloud
- Automatically triggered tasks
	- Source code-triggered: monitors a repository and builds a new image for each commit
	- Base image-triggered: tracks any public docker image repository for new updates to a tag and builds a dependant image
	- Timer triggered: self-explanatory
- Multi step tasks
	- Prety much a full pipeline that can build and run docker images, publish and upgrade helm charts, etc
### Platforms