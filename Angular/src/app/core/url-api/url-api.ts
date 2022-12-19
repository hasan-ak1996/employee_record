const UrlApi = {
     BaseUrl : 'https://localhost:44334',
}
///////Departments//////////
export const Departments = {
     GetAll: UrlApi.BaseUrl + '/api/Department/GetAll',
}

//////Employees////////////
export const Employees = {
     GetAll: UrlApi.BaseUrl + '/api/Employee/GetAll',
     Create: UrlApi.BaseUrl + '/api/Employee/Add',
     CreateWithGetId: UrlApi.BaseUrl + '/api/Employee/AddWithGetId',
     Update:UrlApi.BaseUrl + '/api/Employee/Update',
     Delete:UrlApi.BaseUrl + '/api/Employee/Delete',
     GetByIdWithNavigartionProperty:UrlApi.BaseUrl + '/api/Employee/GetByIdWithNavigartionProperty',
}

//////EmployeeFiles//////
export const EmployeeFiles = {
     Create: UrlApi.BaseUrl + '/api/EmployeeFile/CreateList',
}