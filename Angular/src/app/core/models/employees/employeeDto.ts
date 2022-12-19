import { employeeFilesDto } from "../employeeFiles/employeeFilesDto";

export interface employeeDto {
    id: number;
    name: string;
    departmentId: number;
    dateOfBirth: string;
    address: string;
    departmentName: string;
    creationTime: Date;
    employeeFiles: employeeFilesDto[];
}