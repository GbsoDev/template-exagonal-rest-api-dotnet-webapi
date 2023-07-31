#!/bin/bash

# Elementos de la lista
elements=("Domain" "Application" "InMemory" "MSSQL" "EfCore" "Dtos" "Infrastructure" "Api")

# Bucle para iterar sobre los elementos de la lista
for element in "${elements[@]}"; do
    echo "Ejecutando comandos para ${element}"
    git add "*.${element}/*User*" && git commit -m "User - ${element}"
    git add "*.${element}/*" && git commit -m "${element}"
    echo "---------------"
done
