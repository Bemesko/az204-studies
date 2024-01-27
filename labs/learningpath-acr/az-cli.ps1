$registryName

# equivalent to docker build
az acr build --image sample/hello-world:v1 --registry $registryName --file Dockerfile .

# list repositories in a container registry
az acr repository list --name $registryName

# list tags for a specific image
az acr repository show-tags --name $registryName `
    --repository sample/hello-world

# run the image in the cloud and get the output on your console
az acr run --registry $registryName `
    --cmd '$Registry/sample/hello-world:v1' /dev/null