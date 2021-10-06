import Vue from 'vue'
import VueI18n from 'vue-i18n'

Vue.use(VueI18n)

const messages = {
    en: {
        common: {
            errorMessage: "Error occurred. Please try again.",
            loadingMessage: "Loading",
            fieldNames: {
                createDate: "Create date",
                actions: "Actions",
            },
            buttons: {
                details: "Details",
                edit: "Edit",
                delete: "Remove",
                addRow: "Add row"
            },
            sectionNames: {
                general: "General"
            }
        },
        env: {
            header: "Environments",
            detailsHeader: 'Environment: {id}',
            createHeader: 'Create new environment',
            editHeader: 'Edit environment',
        },
        systems: {
            header: "Systems",
            detailsHeader: 'System: {name}',
            createHeader: "Create new system",
            editHeader: 'Edit system',
        },
        applications: {
            header: "Applications",
            detailsHeader: "Application: {name}",
            createHeader: "Create new application",
            editHeader: "Edit application: {name}",
            versionsHeader: "Versions",
            endpointsHeader: "Endpoints"
        },
        applicationVersions: {
            detailsHeader: "{applicationName} version: [{environmentId}] {version}",
            createHeader: "Create new application version",
        },
        applicationVersionDependencies: {
            dependenciesHeader: "Dependencies",
            endpointsHeader: "Endpoint analysis",
        },
        vlans: {
            header: "Vlans",
            detailsHeader: "Vlan {cidr}",
            machinesHeader: "Machines",
            createHeader: "Create new VLAN",
        },
        machines: {
            header: "Machines",
            detailsHeader: "Machine: {name}",
            networkInterfacesHeader: "Network interfaces",
            dataVolumesHeader: "Data volumes",
        },
        loadBalancers: {
            header: "Load balancers",
            detailsHeader: "Load balancer: {name}",
            membersHeader: "Pools & members"
        },
        firewallRules: {
            header: "Firewall rules",
            detailsHeader: "Load balancer: {name} "
        },
        nugetPackages: {
            header: "Nuget packages",
            versionsHeader: "Versions",
            applicationsHeader: "Applications",
        },
        nugetPackageVersions: {
            header: "Nuget package versions for:",
            detailsHeader: "{name}: {version}",
            applicationsHeader: "Applications",
        },
        endpointAnalysis: {
            drillDownHeader: "Endpoint relations - down",
            drillUpHeader: "Endpoint relations - up",
        },
        redis: {
            header: "Redis",
            detailsHeader: "Redis: {name}",
            createHeader: "Create new Redis",
            editHeader: "Edit Redis: {name}",
            endpointsHeader: "Endpoints",
        },
    }
}

// Create VueI18n instance with options
export default new VueI18n({
    locale: 'en', // set locale
    messages, // set locale messages
})