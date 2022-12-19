export class MenuItem{
    label!: string;
    route!: string;
    icon!: string;
    permissionName?: string;
    children?: MenuItem[];
    constructor(
        label:string,
        route:string,
        icon:string,
        premissionName?:string,
        children?:MenuItem[]
    ) {
        this.label = label;
        this.route = route;
        this.icon = icon;
        this.permissionName = premissionName;
        this.children = children;
    }
}