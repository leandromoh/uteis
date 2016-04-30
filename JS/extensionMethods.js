Array.prototype.indexOfObject = function(obj){
    var json = JSON.stringify(obj);
    var i = this.length;
    while (i--) {
        if (JSON.stringify(this[i]) === json){
            return i;
        }
    }
    return -1;
}

Array.prototype.contains = function(elem){
    var index = typeof elem === 'object' ? this.indexOfObject(elem) : this.indexOf(elem);
    return (index >= 0);
}

Array.prototype.count = function(f){
    var validos = 0;
    
    for(var i = 0, len = this.length; i < len; i++)
        if(f(this[i]) === true)
            validos++;
    
    return validos;
}

Array.prototype.distinct = function(){
    var temp = {};
    for(var i = 0, len = this.length; i < len; i++)
        temp[JSON.stringify(this[i])] = true;
    var r = [];
    for (var key in temp)
        r.push(JSON.parse(key));
    return r;
}
