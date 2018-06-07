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

export type AuthGroup = 'FULL_ACCESS' | 'READ_ACCESS';

export class RequiredRoles {
    public static get admin() { return 'Admin'; }
    public static get user() { return 'User'; }
    public static get all() {
        return [
            { name: RequiredRoles.admin, description: 'Все разделы, чтение и изменение' },
            { name: RequiredRoles.user, description: 'Все разделы, кроме учетных записей, только чтение' }
        ];
    }
}

export class AuthGroups {
    public static get fullAccess() { return 'FULL_ACCESS'; }
    public static get readOnlyAccess() { return 'READ_ONLY_ACCESS'; }

    public static byRole(role: string): string[] {
        if (role === RequiredRoles.admin) { return [ this.fullAccess ]; }
        if (role === RequiredRoles.user) { return [ this.readOnlyAccess ]; }
    }
}

export class AccountActivity {
    id: number;
    accountId: number;
    account: Account;
    ipAddress: string;
    operation: string;
    at: Date;
}

export class Account {
    id: number;
    userName: string;
    password: string;
    role: string = null;
    avatarUri: string;
    personId: number;
    registeredAt: Date;
}
