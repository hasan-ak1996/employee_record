export class CurrentUser{
     public id!:string;
     public firstName!:string;
     public lastName!:string;
     public fullName!:string;
     public email!:string;
     public role!:string;
     public avatar!:string;
     public phoneNumber!:string;
     public Permissions!:Array<string>;


     constructor(id?:string,firstName?:string,lastName?:string,fullName?:string,email?:string,role?:string,avatar?:string,phoneNumber?:string,permissions?:Array<string>) {
        this.id=id ||'';
        this.firstName=firstName||'';
        this.lastName=lastName||'';
        this.fullName=fullName||'';
        this.email=email||'';
        this.role=role||'';
        this.avatar=avatar||'';
        this.phoneNumber=phoneNumber+'';
        this.Permissions=permissions || new Array<string>();
    }/**
     *
     */
}
/**
 *
 */
