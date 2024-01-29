## Resources
- [Preparing for AZ-204 - Develop Azure compute solutions (1 of 5) | Microsoft Learn](https://learn.microsoft.com/en-us/shows/exam-readiness-zone/preparing-for-az-204-develop-azure-compute-solutions-1-of-5)
- [Run container images in Azure Container Instances - Training | Microsoft Learn](https://learn.microsoft.com/en-us/training/modules/create-run-container-images-azure-container-instances/)
## Key Questions
- What is Azure Container Instances?
- When should I use Azure Container Instances
- When should I not use Azure Container Instances?
- What are container groups and when should I use them?
- What are the types of supported volumes for my containers?
- How does size allocation work for multiple containers?
- How is pricing calculated?
- How does networking work?
- What are policies and why should I use them?
- How can I set environment variables in my containers?
- How can I expose containers to the internet?
### My Answers
- What is Azure Container Instances?
	- A service that runs containers but offers few customization options and lacks many orchestration features.
- When should I use Azure Container Instances
	- You have a single containerized workload and you don't want to keep maintaining a Kubernetes or VM infrastructure to support it
- When should I not use Azure Container Instances?
	- You have many containers, need for o more specific customization options or think it's just too expensive
- What are container groups and when should I use them?
	- Container groups are the ACI version of Pods. All containers are deployed to a container group and a linux container group can have more than 1 container
- What are the types of supported volumes for my containers?
	- Azure files
	- Secrets
	- Empty Directory
	- GitHub Repository
- How does size allocation work for multiple containers?
	- The requests for all containers in the group is ADDED, so it is the sum of all requests
- How is pricing calculated?
	- ACI bills based on time the containers spent running x the amount of processing it required
	- Sort of a serverless model, is worth more if the container shouldn't run 24/7 and only during specific intervals. But then you might as well just use Functions
- How does networking work?
	- You expose containers' ports and all containers in the group can access these exposed ports. Some ports can be exposed only internally
- What are restart policies and why should I use them?
	- Restart policies control when and if containers restart
	- Always (default), Never or OnFailure
- How can I set environment variables in my containers?
	- Through the ARM/YAML definition or through the `az container create` command
	- Secrets can be passed with `secretValue`
- How can I expose containers to the internet?
	- You choose a port in the container group to be publicly available and that port starts listening with the FQDN you assign to the container instance.

---
## Learning Path Notes
```yaml
apiVersion: 2018-10-01
location: eastus
name: securetest
properties:
  containers:
  - name: mycontainer
    properties:
      environmentVariables:
        - name: 'NOTSECRET'
          value: 'my-exposed-value'
        - name: 'SECRET'
          secureValue: 'my-secret-value'
      image: nginx
      ports: []
      resources:
        requests:
          cpu: 1.0
          memoryInGB: 1.5
  osType: Linux
  restartPolicy: Always
tags: null
type: Microsoft.ContainerInstance/containerGroups
```
*Sample YAML manifest for a container instance*
### Container Groups
- multicontainer groups only in linux
- container groups are like kubernetes pods
- you usually only put more than one container in a group for init containers or sidecards
- there are reasons to put a frontend in a container and a backend in another, as well
### Deployment
- arm templates/bicep?: better for deploying other stuff along with ACI
- yaml: better for deploying ACI only
### Storage
- supported volumes
	- azure files
	- git repo
	- empty dir
	- secret
### Restart Policies
- always (default)
- never
- onfailure
### Environment Variables
- regular variables are passed like the --env flag in docker run
- secure variables are passed as `secureValue` instead of `value` in the yaml definition
### Mounting File Shares
- smb
- limitations
	- only linux
	- container needs to run as root (bad!)
	- cifs only
- pretty much like a kubernetes yaml or docker compose, for file shares you specify an account name, key and share name and where on the container it will be mounted

---
## Exam Prep Notes
![](media/Pasted%20image%2020240129084243.png)
- why should I use it
- when should I use aks instead of aci
![](media/Pasted%20image%2020240129084300.png)
- arm template is recommended when deploying other things, yaml is recommended for only aci deployment
- types of supported storage volumes
- scenarios for multi-container groups