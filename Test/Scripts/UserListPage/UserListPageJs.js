﻿

$(document).ready(function () {
    debugger;
    $.ajax({
        type: 'get',
        url: '/Ws.asmx/GetUsers',
        data: '',
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (users) {
            CreateUserTable(users.d)

        },
        error: function (users) {
        }

    });
    $('#BtnNewUser').click(function () {
        window.location.href = 'RegistrationPage.html';
    })
        ;
    function CreateUserTable(users) {
        debugger;
        var tbody = $('#Tbody');
        for (var i = 0; i < users.length; i++) {
            tbody.append("<tr><th scope ='row' > " + users[i].Id + "</th> <td>" + users[i].FullName + "</td><td>" + users[i].Phone + "</td> <td>" + new Date(parseInt(users[i].Birthday.substr(6))) + "</td><td>" + users[i].Email + "</td> <td>" + GetGenderText(users[i].Gender) + "</td>  </tr >");
        }
        function GetDate(date) {
            var tdate = new Date(date);
            var dd = tdate.getDate();
            var MM = tdate.getMonth();
            var yyyy = tdate.getFullYear();
            var currentDate = dd + "/" + (MM + 1) + "/" + yyyy;

            return currentDate;
        }
        function GetGenderText(gender) {
            return gender == 0 ? "Male" : "Female"
        }
    }
});