function formToJSON(form){

	var getInputValue = function(elem){
		var type = elem.type;
		
		if(type === 'hidden' || type === 'submit' || type === 'reset')
			return '';
		
		return getValue(elem);
    }
	
	var getValue = function(elem){
    		return '"' + (elem.id || elem.name) + '" : "'  + elem.value + '"';
    }
	
	var getArray = function (tagName, func){
		return [].map.call(form.getElementsByTagName(tagName), func);
    }
    
    var inputs = getArray("input", getInputValue);
    var textareas = getArray("textarea", getValue);
    var selects = getArray("select", getValue);

    var props = inputs.concat(textareas, selects).filter(function(el){
		return el != '';
	});
	
	return "{" + props.join(', ') + "}";
}
