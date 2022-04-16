export default interface ApplicationDetails{
    id: string;
    buildProcessUrls: string;
    name: string;
    code: string;
    projectName: string;
    projectId: string;
    framework: string;
    owner: string;
    repositoryUrl: string;
    description: string;
    createDate: string;
    endpoints: Array<Endpoint>;
}

export interface  Endpoint {
    environmentId: string;
    path: string;
    comment: string;
}


