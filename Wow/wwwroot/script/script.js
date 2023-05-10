function showUser(){
    doc = document.location.href("Shared/MainLayout.razor");
    //doc.getElementById("signin").visibility="hidden";
    const h1= doc.querySelector('a');
    const parent = h1.parentNode
    parent.removeChild(h1)
};