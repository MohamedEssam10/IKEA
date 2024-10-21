// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var searchInput = document.getElementById("searchInput");

searchInput.addEventListener("keyup", function () {
    var searchValue = searchInput.value;
    var xhr = new XMLHttpRequest();

    // Use backticks for template literals
    xhr.open("GET", `/Employee/Index?search=${searchValue}`, true);
    xhr.send();

    xhr.onreadystatechange = function () {
        if (xhr.readyState == XMLHttpRequest.DONE) {
            if (xhr.status == 200) {
                // Replace only the employee list part
                document.getElementById("employeeList").innerHTML = xhr.responseText;
            } else if (xhr.status == 400) {
                alert('There was an error 400.');
            } else {
                alert('Something else other than 200 returned.');
            }
        }
    };
});


