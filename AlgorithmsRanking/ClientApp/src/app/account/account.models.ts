export class UserInfo {
    id: number;
    personId: number;
    userName: string;
    displayName: string;
    role: string;
    avatarUri: string;
    email: string;
    phone: string;
}

export class UserCredentials {
    userName: string;
    password: string;
    rememberMe: boolean;
}
