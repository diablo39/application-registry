import Vue from 'vue'
import VueRouter, {RouteConfig} from 'vue-router'
import Home from '@/views/Home.vue'
import Callback from '@/views/Callback.vue'
Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
    {
        path: '/',
        name: 'Home',
        component: Home
    },
    {
        path: '/oidc/callback',
        name: 'Callback',
        component: Callback,
        meta: {
            anonymousAccess: true
        },
    },
    {
        path: '/unauthorized',
        name: 'Unauthorized',
        component: () => import(/* webpackChunkName: "about" */ '../views/Unauthorized.vue'),
        meta: {
            anonymousAccess: true
        },
    },
// Env ---------------------------------------------
    {
        path: '/env',
        name: 'Environments',
        component: () => import(/* webpackChunkName: "env" */ '../views/env/List.vue'),
    },
    {
        path: '/env/create',
        name: 'EnvironmentCreate',
        component: () => import(/* webpackChunkName: "env" */ '../views/env/Create.vue'),
    },
    {
        path: '/env/:id',
        name: 'EnvironmentDetails',
        component: () => import(/* webpackChunkName: "env" */ '../views/env/Details.vue')
    },
    {
        path: '/env/:id/edit',
        name: 'EnvironmentEdit',
        component: () => import(/* webpackChunkName: "env" */ '../views/env/Edit.vue')
    },
// Projects --------------------------------------------------------
    {
        path: '/systems',
        name: 'Projects',
        component: () => import(/* webpackChunkName: "systems" */ '../views/systems/List.vue'),
    },
    {
        path: '/systems/create',
        name: 'ProjectCreate',
        component: () => import(/* webpackChunkName: "systems" */ '../views/systems/Create.vue'),
    },
    {
        path: '/systems/:id',
        name: 'ProjectDetails',
        component: () => import(/* webpackChunkName: "systems" */ '../views/systems/Details.vue'),
    },
    {
        path: '/systems/:id/edit',
        name: 'ProjectEdit',
        component: () => import(/* webpackChunkName: "systems" */ '../views/systems/Edit.vue'),
    },

    //Applications ---------------------------------
    {
        path: '/applications',
        name: 'ApplicationList',
        component: () => import(/* webpackChunkName: "applications" */ '../views/applications/List.vue'),
    },
    {
        path: '/applications/create',
        name: 'ApplicationCreate',
        component: () => import(/* webpackChunkName: "applications" */ '../views/applications/Create.vue'),
    },
    {
        path: '/applications/:id/details',
        name: 'ApplicationDetails',
        component: () => import(/* webpackChunkName: "applications" */ '../views/applications/Details.vue'),
    },
    {
        path: '/applications/:id/edit',
        name: 'ApplicationEdit',
        component: () => import(/* webpackChunkName: "applications" */ '../views/applications/Edit.vue'),
    },
    {
        path: '/applications/:applicationId/application-versions/create',
        name: 'ApplicationVersionCreate',
        component: () => import(/* webpackChunkName: "application-versions" */ '../views/application-versions/Create.vue'),
    },
    {
        path: '/applications/:applicationId/application-versions/:id/details',
        name: 'ApplicationVersionDetails',
        component: () => import(/* webpackChunkName: "application-versions" */ '../views/application-versions/Details.vue'),
    },
    {
        path: '/applications/:applicationId/application-versions/:applicationVersionId/swagger',
        name: 'ApplicationVersionSwagger',
        component: () => import(/* webpackChunkName: "application-version-specification-swagger" */ '../views/application-versions/specyfications/SpecificationSwagger.vue'),
    },
// VLAN
    {
        path: '/vlans',
        name: 'VlanList',
        component: () => import(/* webpackChunkName: "vlans" */ '../views/vlans/List.vue'),
    },
    {
        path: '/vlans/:id/details',
        name: 'VlanDetails',
        component: () => import(/* webpackChunkName: "vlans" */ '../views/vlans/Details.vue'),
    },
    {
        path: '/vlans/create',
        name: 'VlanCreate',
        component: () => import(/* webpackChunkName: "vlans" */ '../views/vlans/Create.vue'),
    },
    {
        path: '/vlans/:id/edit',
        name: 'VlanEdit',
        component: () => import(/* webpackChunkName: "vlans" */ '../views/vlans/Edit.vue'),
    },
    // load-balancers
    {
        path: '/load-balancers',
        name: 'LoadBalancersList',
        component: () => import(/* webpackChunkName: "load-balancers" */ '../views/load-balancers/List.vue'),
    },
    {
        path: '/load-balancers/:id',
        name: 'LoadBalancersDetail',
        component: () => import(/* webpackChunkName: "load-balancers" */ '../views/load-balancers/Details.vue'),
    },
    // firewall-rules
    {
        path: '/firewall-rules',
        name: 'FirewallRulesList',
        component: () => import(/* webpackChunkName: "firewall-rules" */ '../views/firewall-rules/List.vue'),
    },
    // machines
    {
        path: '/machines',
        name: 'MachinesList',
        component: () => import(/* webpackChunkName: "machines" */ '../views/machines/List.vue'),
    },
    {
        path: '/machines/:id',
        name: 'MachineDetails',
        component: () => import(/* webpackChunkName: "machines" */ '../views/machines/Details.vue'),
    },
    // redis
    {
        path: '/redis',
        name: 'RedisList',
        component: () => import(/* webpackChunkName: "redis" */ '../views/redis/List.vue'),
    },
    {
        path: '/redis/:id',
        name: 'RedisDetails',
        component: () => import(/* webpackChunkName: "redis" */ '../views/redis/Details.vue'),
    },
    // nuget packages
    {
        path: '/nuget-packages',
        name: 'NugetPackageList',
        component: () => import(/* webpackChunkName: "nuget" */ '../views/nuget-packages/List.vue'),
    },
    {
        path: '/nuget-packages/:id/details',
        name: 'NugetPackageDetails',
        component: () => import(/* webpackChunkName: "nuget" */ '../views/nuget-packages/Details.vue'),
    },
    {
        path: '/nuget-packages/:name/:version/details',
        name: 'NugetPackageVersionDetails',
        component: () => import(/* webpackChunkName: "nuget" */ '../views/nuget-packages/VersionDetails.vue'),
    },
    {
        path: '/endpoint-analysis/drill-down/:version',
        name: 'EndpointAnalysisDrillDown',
        component: () => import(/* webpackChunkName: "nuget" */ '../views/endpoint-analysis/DrillDown.vue'),
    },
    {
        path: '/endpoint-analysis/drill-up/:version',
        name: 'EndpointAnalysisDrillUp',
        component: () => import(/* webpackChunkName: "nuget" */ '../views/endpoint-analysis/DrillUp.vue'),
    },
    // {
    //   path: '/about',
    //   name: 'About',
    //   // route level code-splitting
    //   // this generates a separate chunk (about.[hash].js) for this route
    //   // which is lazy-loaded when the route is visited.
    //   component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
    // }
]

const router = new VueRouter({
    routes,
    mode: 'history',
})

const DEFAULT_TITLE = 'Application registry';
router.afterEach((to, from) => {
    // Use next tick to handle router history correctly
    // see: https://github.com/vuejs/vue-router/issues/914#issuecomment-384477609
    Vue.nextTick(() => {
        document.title = to.meta.title || DEFAULT_TITLE;
    });
});

router.beforeEach(async (to, from, next) => {
    Vue.nextTick() // required for first loading
        .then(function () {
            const appInstance = router.app as any;
            const app = router.app.$data || { isAuthenticated: false} ;

            if (app.isAuthenticated) {
                //already signed in, we can navigate anywhere
                next();
            } else if (to.matched.some(record => ! record.meta.anonymousAccess)) {
                //authentication is required. Trigger the sign in process, including the return URI
                Vue.nextTick(function(){
                    appInstance.authenticate(to.path).then(() => {
                        console.log('authenticating a protected url:' + to.path);
                        if (app.isAuthenticated) {
                            //already signed in, we can navigate anywhere
                            next();
                        }
                    });
                });
            } else {
                //No auth required. We can navigate
                next()
            }
        })


});

export default router
