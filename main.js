new Vue({
    el: '#app',
    vuetify: new Vuetify(),
    data() {
        return {
            tab: 0, // 初期タブ
            dataList: [], // 会員登録情報を格納するリスト
        };
    },
    methods: {
        async fetchData() {
            try {
                const response = await axios.get('https://m3h-yuunaminagawa.azurewebsites.net/api/SELECT');
                // 結果をdataListに格納
                this.dataList = response.data;
                console.log(this.dataList);
            } catch (error) {
                console.error("APIの取得に失敗しました", error);
            }
        },
        addData() {
            // Googleの検索画面に遷移
            window.location.href = 'https://www.google.com';
        },
    },
    mounted() {
        // マウント時にデータを取得
        this.fetchData();
    },
});
