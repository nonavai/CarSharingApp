import {UserInfo} from "./UserInfo";

export interface UserData{
    id: string,
    firstName: string,
    lastName: string,
    email: string,
    password: string,
    phoneNumber: string,
    recordNumber: string,
    userName: string,
    userInfo: UserInfo
}