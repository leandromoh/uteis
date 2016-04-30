function formToJSON(form){
    var obj = {};

    var getInputValue = function(elem){
        var type = elem.type;
        if( type === 'hidden' || 
            type === 'submit' || 
            type === 'reset')
            return;

        getValue(elem);    
    }

    var getValue = function(elem){
        obj[(elem.id || elem.name)] = elem.value;
    }

    var getArray = function(tagName, func){
        [].map.call(form.getElementsByTagName(tagName), func);
    }

    getArray("input", getInputValue);
    getArray("textarea", getValue);
    getArray("select", getValue);

    return JSON.stringify(obj);
}
