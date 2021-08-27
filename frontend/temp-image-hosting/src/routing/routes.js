import Home from '../views/Home.vue';
import Success from '../views/Success.vue';


const routes = [
    { path: '/success', component: Success, name: "Success" },
    { path: '/', component: Home, name: "Home" }
]

export default routes;