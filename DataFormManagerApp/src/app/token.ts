export class Token {

    TokenId:number;
    AccessToken:string;
    RefreshToken:string;
    ExpiresIn:string;
    AuthorizationCode:string;
}

export class CodeObject {
    code : string;
}

export class RefreshTokenObject {
    RefreshToken : string;
}