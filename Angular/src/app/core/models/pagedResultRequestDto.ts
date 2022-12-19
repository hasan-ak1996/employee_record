export class PagedResultRequestDto {
    skipCount: number = 0;
    maxResult: number = 2147483647;
    keyword!:string | null;
}