import axios from "axios";
import global from '@/global'
import {PagingInfo} from './PagingInfo'

class HttpClientImplementation {

    HTTP = axios.create({
        //baseURL: `https://localhost:5011/`,
        // headers: {
        //   Authorization: 'Bearer {token}'
        // }
    })

    private adjustPagingInfo(pagingInfo: PagingInfo, useServerSidePagination): PagingInfo {
        const result = pagingInfo.clone();

        if (!useServerSidePagination) {
            result.itemsPerPage = -1;
            result.page = 1;
            result.sortBy = [];
            result.sortDesc = [];
        }

        return result;
    }

    private pagingInfoToQueryStringParams(args: PagingInfo, useServerSidePagination = global.config.useServerSidePagination) {

        const pagingInfo = this.adjustPagingInfo(args, useServerSidePagination);

        // eslint-disable-next-line
        const params: any = {};

        if (pagingInfo.itemsPerPage !== -1) {
            params.page = pagingInfo.page;
            params.itemsPerPage = pagingInfo.itemsPerPage
        }

        if (pagingInfo.sortBy.length > 0 && pagingInfo.sortDesc.length > 0) {
            params.sortBy = pagingInfo.sortBy[0]
            params.sortDesc = pagingInfo.sortDesc[0]
        }

        return params;
    }


    get(path: string, pagingInfo: PagingInfo = PagingInfo.Default, useServerSidePagination = global.config.useServerSidePagination) {
        return this.HTTP.get(path, {params: this.pagingInfoToQueryStringParams(pagingInfo, useServerSidePagination)});
    }

    getApplicationsPath = `/api/Applications`;

    getApplications(pagingInfo: PagingInfo = PagingInfo.Default) {
        return this.HTTP.get(`/api/Applications`, {params: this.pagingInfoToQueryStringParams(pagingInfo)});
    }

    getApplicationDetails(id: string) {
        return this.HTTP.get(`/api/Applications/${id}`);
    }

    createApplication(application) {
        return this.HTTP.post(`/api/Applications`, application);
    }

    updateApplication(application) {
        return this.HTTP.put(`/api/Applications/${application.id}`, application);
    }

    getApplicationVersionsPath(idApplication: string) {
        return `/api/Applications/${idApplication}/versions`;
    }

    getApplicationVersions(idApplication: string, pagingInfo: PagingInfo = PagingInfo.Default) {
        return this.HTTP.get(`/api/Applications/${idApplication}/versions`, {params: this.pagingInfoToQueryStringParams(pagingInfo)});
    }

    getApplicationVersionDetails(idApplicationVersion: string) {
        return this.HTTP.get(`/api/ApplicationVersions/${idApplicationVersion}`);
    }

    createApplicationVersion(applicationVersion) {
        return this.HTTP.post(`/api/ApplicationVersions`, applicationVersion);
    }
    getEnvironmentsPath = `/api/Environments`;

    getEnvironment(id: string) {
        return this.HTTP.get(`/api/Environments/${id}`);
    }

    getEnvironments() {
        return this.HTTP.get(this.getEnvironmentsPath);
    }
    createEnvironment(env: object) {
        return this.HTTP.post(`/api/Environments`, env);
    }

    updateEnvironment(env) {
        return this.HTTP.put(`/api/Environments/${env.id}`, env);
    }

    getProjectsPath = `/api/Projects`

    getProjects(pagingInfo: PagingInfo = PagingInfo.Default) {
        return this.HTTP.get(`/api/Projects`, {params: this.pagingInfoToQueryStringParams(pagingInfo)});
    }

    getProject(id: string) {
        return this.HTTP.get(`/api/Projects/${id}`);
    }

    createProject(project) {
        return this.HTTP.post(`/api/Projects`, project);
    }

    updateProject(project) {
        return this.HTTP.put(`/api/Projects/${project.id}`, project);
    }

    getVlansPath = `/api/Vlans`;

    getVlan(id: string) {
        return this.HTTP.get(`/api/Vlans/${id}`);
    }

    createVlan(vlan: object) {
        return this.HTTP.post(`/api/Vlans`, vlan);
    }

    updateVlan(vlan) {
        const id = vlan.id;
        delete  vlan.id;
        return this.HTTP.put(`/api/Vlans/${id}`, vlan);
    }

    getMachinesPath = `/api/Machines`;

    getMachine(id: string) {
        return this.HTTP.get(`/api/Machines/${id}`);
    }

    getLoadBalancersPath = `/api/LoadBalancers`;

    getLoadBalancer(id: string) {
        return this.HTTP.get(`/api/LoadBalancers/${id}`);
    }

    getFirewallRulesPath = `/api/FirewallRules`;
    getNugetPackagesPath = `/api/NugetPackages`;
    getNugetPackageVersionsPath(id: string) {
        return `/api/NugetPackages/${id}/versions`;
    }
    getNugetPackageApplicationsPath(id: string) {
        return `/api/NugetPackages/${id}/applications`;
    }
    getNugetPackageDetails(packageName: string, packageVersion) {
        return this.HTTP.get(`/api/NugetPackages/${packageName}/${packageVersion}`);
    }

    getEndpointAnalysisDrillDown(httpMethod, path, version): string {
        return `/api/EndpointAnalysis/DrillDown?httpMethod=${httpMethod}&path=${path}&idVersion=${version}`;
    }

    getEndpointAnalysisDrillUp(httpMethod, path, version): string {
        return `/api/EndpointAnalysis/DrillUp?httpMethod=${httpMethod}&path=${path}&idVersion=${version}`;
    }

    getRedis(id: string) {
        return this.HTTP.get(`/api/Redis/${id}`);
    }

    getRedisPath = `/api/Redis`;
}

export const HttpClient = new HttpClientImplementation()
export {PagingInfoData} from './PagingInfoData'
export {PagingInfo} from './PagingInfo'


