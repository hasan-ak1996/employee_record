import { employeeFilesDto } from "../employeeFiles/employeeFilesDto";

export interface updateEmployeeDto {
    id: number;
    name: string;
    departmentId: number;
    dateOfBirth: string;
    address: string;
    deletedEmployeeFiles: employeeFilesDto[];
}