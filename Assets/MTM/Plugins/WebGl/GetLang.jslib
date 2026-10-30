mergeInto(LibraryManager.library, {
  GetLang: function() {
    var lang = window.lang || "ru";
    var lengthBytes = lengthBytesUTF8(lang) + 1;
    var stringOnWasmHeap = _malloc(lengthBytes);
    stringToUTF8(lang, stringOnWasmHeap, lengthBytes);
    return stringOnWasmHeap;
  }
});
