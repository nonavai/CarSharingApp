import {LicenceType} from "../Enums/LicenceType";

export interface UserInfo{
    id: string,
    userId: string,
    birth: Date,
    country: string,
    firstName: string,
    lastName: string,
    category: LicenceType,
    licenceExpiry: Date,
    licenceIssue: Date,
    licenceId: string,
    placeOfIssue: string,
}