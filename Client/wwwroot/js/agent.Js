$(document).ready(function () {
    function loadFormModal() {
        $("#formModal").modal("show");
    }

    function validateForm() {
        var form = document.getElementById("myForm");
        if (form.checkValidity() === false) {
            event.preventDefault();
            event.stopPropagation();
            form.classList.add('was-validated');
            return false;
        }
        return true;
    }

    $("#submitFormBtn").click(function () {
        if (validateForm()) {
            console.log("Form submitted successfully");
            $("#formModal").modal("hide");
        }
    });

    $.ajax({
        url: "https://localhost:7264/api/Employee/",
        method: "GET"
    }).done(function (response) {
        var dataAll = response.data;
        var numb = 0;
        var genderStirng = "";

        var tableHead = $("<thead></thead>");
        var headRow = $("<tr><th>No</th><th>NIK</th><th>First Name</th><th>Last Name</th><th>Birthdate</th><th>Gender</th><th>Hiring Date</th><th>Email</th><th>Phone Number</th><th>Action</th></tr>");
        tableHead.append(headRow);
        $("#valoTb").append(tableHead);

        $.each(dataAll, function (index, data) {
            var row = $("<tr></tr>");
            var numberCell = $("<td></td>").text(numb + 1);
            row.append(numberCell);

            var displayNameCell1 = $("<td></td>").text(data.nik);
            row.append(displayNameCell1);
            var displayNameCell2 = $("<td></td>").text(data.firstName);
            row.append(displayNameCell2);
            var displayNameCell2 = $("<td></td>").text(data.lastName);
            row.append(displayNameCell2);
            var displayNameCell3 = $("<td></td>").text(data.birthDate);
            row.append(displayNameCell3);
            if (data.gender == 1) {
                genderStirng = "Male";
            }else {
            genderStirng = "Female"; 
            }
            var displayNameCell4 = $("<td></td>").text(genderStirng);
            row.append(displayNameCell4);
            var displayNameCell5 = $("<td></td>").text(data.hiringDate);
            row.append(displayNameCell5);
            var displayNameCell6 = $("<td></td>").text(data.email);
            row.append(displayNameCell6);
            var displayNameCell7 = $("<td></td>").text(data.phoneNumber);
            row.append(displayNameCell7);

            var actionCell = $("<td></td>");
            var addActionBtn = $("<button></button>", {
                text: "Insert",
                class: "btn btn-success btn-sm",
                click: function () {
                    addEmployee();
                }
            });
            actionCell.append(addActionBtn);

            var editBtn = $("<button></button>", {
                text: "Update",
                class: "btn btn-primary btn-sm",
                click: function () {
                    editData(data);
                }
            });
            actionCell.append(editBtn);

            var deleteBtn = $("<button></button>", {
                text: "Delete",
                class: "btn btn-danger btn-sm",
                click: function () {
                    deleteData(data);
                }
            });
            actionCell.append(deleteBtn);

            row.append(actionCell);

            $("#valo").append(row);
            numb++;
        });

        $("#valoTb").DataTable({
            dom: 'Bfrtip',
            buttons: [
                'copy', 'csv', 'excel', 'pdf', 'print'
            ]
        });

    }).fail(function () {
        alert("Error retrieving employee data.");
    });

    function addEmployee() {
        $("#employeeModal").modal("show");
    }


    function editData(data) {
        // Show the form modal
        $("#formModal").modal("show");
        // Populate the form fields with the agent's data
        $("#firstname").val(data.firstName);
        $("#lastname").val(data.lastName);
        $("#birthdate").val(data.birthDate);
        $("input[name='gender'][value='" + data.gender + "']").prop("checked", true);
        $("#hiringdate").val(data.hiringDate);
        $("#email").val(data.email);
        $("#phonenumber").val(data.phoneNumber);

        $("#submitFormBtn").off("click").on("click", () => {
            if (validateForm()) {
                var updatedData = {
                    guid: data.guid,
                    nik: data.nik,
                    firstName: $("#firstname").val(),
                    lastName: $("#lastname").val(),
                    birthDate: $("#birthdate").val(),
                    gender: $("#gender").val(),
                    hiringDate: $("#hiringdate").val(),
                    email: $("#email").val(),
                    phoneNumber: $("#phonenumber").val()
                };

                $.ajax({
                    url: "https://localhost:7264/api/Employee/",
                    method: "PUT",
                    data: JSON.stringify(updatedData),
                    contentType: "application/json",
                    success: function (response) {
                        location.reload(); // Refresh the page to reflect the updated data
                    },
                    error: function () {
                        alert("Error updating employee");
                    }
                });
            }
        });
    }

    function deleteData(data) {
        $('#deleteModal').modal('show');
        $('#deleteModal .modal-body').text('Are you sure you want to delete this agent?');
        $('#deleteModal .btn-confirm').click(function () {
            $.ajax({
                url: "https://localhost:7264/api/Employee/" + data.guid,
                method: "DELETE",
                success: function (response) {
                    location.reload();
                },
                error: function () {
                    alert("Error deleting employee.");
                }
            });
        });
    }

});

function AddEmployee() {
    var eNik = $('#employee-nik').val();
    var eFirst = $('#employee-fname').val();
    var eLast = $('#employee-lname').val();
    var eBDate = $('#employee-bdate').val();
    var eGender = document.querySelector('input[name="employee-gender"]:checked').id.includes('m') ? 1 : 0;
    var eHDate = $('#employee-hdate').val();
    var eEmail = $('#employee-email').val();
    var ePhone = $('#employee-pnumber').val();

    $.ajax({
        async: true, // Async by default is set to “true” load the script asynchronously  
        // URL to post data into sharepoint list  
        url: "https://localhost:7264/api/Employee",
        method: "POST", //Specifies the operation to create the list item  
        data: JSON.stringify({
            '__metadata': {
                'type': 'SP.Data.EmployeeListItem' // it defines the ListEnitityTypeName  
            },
            //Pass the parameters
            'nik': eNik,
            'firstName': eFirst,
            'lastName': eLast,
            'birthDate': eBDate,
            'gender': eGender,
            'hiringDate': eHDate,
            'email': eEmail,
            'phoneNumber': ePhone
        }),
        headers: {
            "accept": "application/json;odata=verbose", //It defines the Data format   
            "content-type": "application/json;odata=verbose", //It defines the content type as JSON  
/*            "X-RequestDigest": $("#__REQUESTDIGEST").val() //It gets the digest value   
*/        },
        success: function (data) {
            console.log(data);
        },
        error: function (error) {
            console.log(JSON.stringify(error));

        }

    })

}