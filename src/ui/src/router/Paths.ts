export class ViewPaths {
    getVlanDetails(id): string {

        return `/vlans/${id.replace('/', '_')}`
    }

    createApplication(): string {
        return '/applications/create';
    }

    getApplicationDetails(id): string {
        return `/applications/${id}/details`
    }

    editApplication(id, source: string): string {
        return `/applications/${id}/edit?source=${source}`;
    }

    getApplicationVersionDetails(applicationId, id): string {
        return `/applications/${applicationId}/application-versions/${id}/details`;
    }

    createEnv(): string {
        return '/env/create';
    }

    getEnvDetails(id): string {
        return `/env/${id}`
    }

    editEnv(id, source: string): string {
        return `/env/${id}/edit?source=${source}`;
    }

    getLoadBalancerDetails(id): string {
        return `/load-balancers/${id}`;
    }

    getMachineDetails(id): string {
        return `/machines/${id}`;
    }

    createSystem(): string {
        return '/systems/create';
    }

    getSystemDetails(id): string {
        return `/systems/${id}`
    }

    editSystem(id, source: string): string {
        return `/systems/${id}/edit?source=${source}`;
    }

    getNugetPackageDetails(id): string {
        return `/nuget-packages/${id}/details`
    }

    getNugetVersionPackageDetails(idPackage, idVersion): string {
        return `/nuget-packages/${idPackage}/${idVersion}/details`;
    }

    getApplicationEndpoints(idVersion): string {
        return `/api/ApplicationVersions/${idVersion}/endpoints`;
    }

    getRedisDetails(id): string {
        return `/redis/${id}`
    }
}

export const Paths = new ViewPaths();

export default Paths;