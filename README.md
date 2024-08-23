# Ceridwen by BogSiren 

Ceridwen, a figure from Welsh mythology, said to be the keeper of the cauldron of knowledge, mother of transformation and the white lady of inspiration and death.


# What is Ceridwen?

An simple application that will run a Large Language Model (LLM) on your local machine, utilizing your GPU (preferably). 

The application itself is a basic dotnet console application that will pass on your questions to an Ollama model by Meta. 


# Prerequisites 

* Docker
* WSL2+

If usisng the GPU:

* Nvidia GPU
* up-to-date GPU drivers 


# How to run Ceridwen

Simply use the following command in the terminal (when in the main project folder):

```
docker compose up -d
```

Afterwards attach to the running console application and get on typing. 

```
docker attach <container_name_or_id>
```

To stop Ceridwen ctrl + C out of the attached window and use the following docker command.

```
docker compose down
```

## Notes 

* Cerdiwen can be run on the CPU instead of the GPU by commenting out the deploy section in the docker-compose.yml, however this will vastly decrease the time required for it to handle the prompts and make it far less useable as a tool. 
* By default llama3 model is used, adjust the environment variable in the docker compose if you want to use a larger/newer model. 



# Resources 

Resources used to create this tiny project include: 

* **Ollama**. https://hub.docker.com/r/ollama/ollama
* **OllamaSharp**. https://github.com/awaescher/OllamaSharp