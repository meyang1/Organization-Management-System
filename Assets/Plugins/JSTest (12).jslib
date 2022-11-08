mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Hello, world!");
  },

  HelloString: function (str) {
    window.alert(UTF8ToString(str));
  },

  PrintFloatArray: function (array, size) {
    for(var i = 0; i < size; i++)
    console.log(HEAPF32[(array >> 2) + i]);
  },


  JSLogin: function (url,formdata){
        
        let xhttp = new XMLHttpRequest();
        var jsURL = Pointer_stringify(url);

        var jsFormData = Pointer_stringify(formdata);



        xhttp.onreadystatechange = function(){

            if(this.readyState == 4 && this.status == 200){

                console.log(xhttp.responseText);
                window.alert(xhttp.responseText);  
                return xhttp.responseText;
            }

        };


        xhttp.open("POST",jsURL,true);

        xhttp.withCredentials = true;

        xhttp.setRequestHeader("Content-type","application/x-www-form-urlencoded");

        xhttp.send(jsFormData);
    },

JSGET: function(url){
    
        let xhttp = new XMLHttpRequest();
        var jsURL = Pointer_stringify(url);
        xhttp.onreadystatechange = function(){

            if(this.readyState == 4 && this.status == 200){

                console.log(xhttp.responseText);
                window.alert(xhttp.responseText);  
            }

        };


        xhttp.open("GET",jsURL);
        xhttp.send();
        
},
  
setCookie: function (cname, cvalue, exdays) {
       var d = new Date();
       d.setTime(d.getTime() + (exdays*24*60*60*1000));
       var expires = "expires="+ d.toUTCString();
       document.cookie = Pointer_stringify(cname) + "=" + Pointer_stringify(cvalue) + expires + ";path=/";
console.log('set cookie='+document.cookie);
    },
 
    getCookie: function (cname) {
       var ret="";
       var name = Pointer_stringify(cname) + "=";
var decodedCookie = decodeURIComponent(document.cookie);
       console.log('get cookie='+decodedCookie);
       var ca = decodedCookie.split(';');
       for(var i = 0; i <ca.length; i++) {
           var c = ca[i];
           while (c.charAt(0) == ' ') {
               c = c.substring(1);
           }
           if (c.indexOf(name) == 0) {
               ret=c.substring(name.length, c.length);
               break;
           }
       }
       var buffer = _malloc(lengthBytesUTF8(ret) + 1);
       writeStringToMemory(ret, buffer);
       return buffer;
    },
  AddNumbers: function (x, y) {
    return x + y;
  },

  StringReturnValueFunction: function () {
    var returnStr = "bla";
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },

  BindWebGLTexture: function (texture) {
    GLctx.bindTexture(GLctx.TEXTURE_2D, GL.textures[texture]);
  },

});