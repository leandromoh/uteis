function getTypedValue(str){
    try{
        return JSON.parse(str);
    }
    catch(ex){
        return str;
    }
}

function queryStringToObject(query){
    if(query[0] == '?')
        query = query.substring(1);

    var obj = {};
    var params = query.split('&');
    var i = params.length;
    var aux;

    while(i--){
        aux = params[i].split('=');
        obj[aux[0]] = getTypedValue(aux[1]);
    }
    
    return obj;
}

var queryString = '?q=class+diagram+recursive+associations&espv=2&biw=1600&pessoa={"nome":"leandro", "idade":19}&bih=799&source=lnms&tbm=isch&sa=X&ved=0ahUKEwi1h92ygZ7MAhUEhJAKHWuoBQEQ_AUIBigB&paises=[1,2,3]';

var obj = queryStringToObject(queryString);

console.log(obj);
console.log(obj["paises"]);
console.log(obj["pessoa"]);
console.log(obj["biw"]);
console.log(obj["q"]);