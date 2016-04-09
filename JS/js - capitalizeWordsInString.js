function capitalizeWord(str)
{
    if(/^[IVXLCDM]+$/i.test(str))
        return str.toUpperCase();

    if(str.length > 3)
    	return str.charAt(0).toUpperCase() + str.slice(1).toLowerCase();
    else
        return str.toLowerCase();
}

function capitalizeWordsInString(str)
{
    return str.split(" ").map(capitalizeWord).join(" ");
}

var x = [
    "ALGORÍTIMOS E LÓGICA DE PROGRAMAÇÃO",
    "LINGUAGEM DE PROGRAMAÇÃO",
    "LINGUAGEM DE PROGRAMAÇÃO IV",
    "LABORATÓRIO DE BANCO DE DADOS",
    "ESTRUTURAS DE DADOS",
    "ENGENHARIA DE SOFTWARE III",
    "PROGRAMAÇÃO ORIENTADA A OBJETOS"
]

var y = x.map(capitalizeWordsInString);

alert(y.join("\n"));
