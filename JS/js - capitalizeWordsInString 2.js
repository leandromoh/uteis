//https://developer.mozilla.org/pt-BR/docs/Web/JavaScript/Reference/Global_Objects/String/replace
var x = [
    "ALGORÍTIMOS E LÓGICA DE PROGRAMAÇÃO",
    "LINGUAGEM DE PROGRAMAÇÃO",
    "LINGUAGEM DE PROGRAMAÇÃO IV",
    "LABORATÓRIO DE BANCO DE DADOS",
    "ESTRUTURAS DE DADOS",
    "ENGENHARIA DE SOFTWARE III",
    "PROGRAMAÇÃO ORIENTADA A OBJETOS"
];

function capitalizeWordsInString(str)
{
   var acentuadasMinusculas = '\\u00E0-\\u00FD';
   var acentuadasMaiusculas = '\\u00C0-\\u00DD';
   var acentuadas = acentuadasMinusculas + acentuadasMaiusculas;
   
   return str
   .replace(/\s/g, function(x){ return x+x; })
   .replace(new RegExp('([a-zA-Z])([\\w'+acentuadas+']{3,})','g'), function(match, p1, p2){ return p1.toUpperCase() + p2.toLowerCase(); })
   .replace(new RegExp('(^|\\s)[a-zA-Z'+acentuadas+']{1,3}(\\s|$)','g'), function(x){ return x.toLowerCase(); })
   .replace(new RegExp('(^|\\s)([IVXLCDM]+)(\\s|$)','gi'), function(x){ return x.toUpperCase(); })
   .replace(/(\s)(\s)/g, function(match, p1, p1){return p1;});
}

var y = x.map(capitalizeWordsInString);

alert(y.join("\n"));
