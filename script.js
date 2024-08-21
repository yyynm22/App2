const app = new Vue({
    el: '#app',
    vuetify: new Vuetify(),
    data: {
        temperature: '',
        season: '',
        dress: '',
        dressURL: '',
        mark: '',
        dataList: [],
    },
    methods: {
        addData: async function () {
            if (!this.temperature || isNaN(this.temperature)) {
                console.log("temperatureに数値が入力されていません");
                return;
            }

            const param = {
                temperature: this.temperature,
                season: this.season,
                dress: this.dress,
                dressURL: this.dressURL,
                mark: this.mark,
            };

            try {
                const response = await axios.post('https://m3h-yuunaminagawa.azurewebsites.net/api/INSERT', param);
                console.log(response.data);
                this.temperature = '';
                this.season = '';
                this.dress = '';
                this.dressURL = '';
                this.mark = '';
            } catch (error) {
                console.error("データの追加に失敗しました:", error);
            }
        },

        deleteData: async function (data) {
            if (!data.temperature) {
                console.log("temperatureに数値が入力されていません");
                return;
            }

            const param = {
                temperature: data.temperature,
                season: data.season,
                dress: data.dress,
                dressURL: data.dressURL,
                mark: data.mark
            };

            try {
                const response = await axios.post('https://m3h-yuunaminagawa.azurewebsites.net/api/DELETE', param);
                console.log(response.data);

                // データ削除が成功した場合に、一覧から該当データを削除
                this.dataList = this.dataList.filter(item => item.temperature !== data.temperature);
            } catch (error) {
                console.error("データの削除に失敗しました:", error);
            }
        },



        readData: async function () {
            try {
                const response = await axios.get('https://m3h-yuunaminagawa.azurewebsites.net/api/SELECT');
                console.log(response.data);
                this.dataList = response.data.List.sort((a, b) => a.temperature - b.temperature);
            } catch (error) {
                console.error("データの取得に失敗しました:", error);
            }
        },

        toggleExpand(card) {
            if (card.isExpanded) {
                card.isExpanded = false;
            } else {
                // 他のカードの拡大を解除
                this.dataList.forEach(item => {
                    item.isExpanded = false;
                });
                card.isExpanded = true;
            }
        }


    },
});

document.addEventListener('DOMContentLoaded', function () {
    const tabs = document.getElementsByClassName('tab');
    for (let i = 0; i < tabs.length; i++) {
        tabs[i].addEventListener('click', tabSwitch, false);
    }

    function tabSwitch() {
        document.getElementsByClassName('is-active')[0].classList.remove('is-active');
        this.classList.add('is-active');
        document.getElementsByClassName('is-show')[0].classList.remove('is-show');
        const arrayTabs = Array.prototype.slice.call(tabs);
        const index = arrayTabs.indexOf(this);
        document.getElementsByClassName('panel')[index].classList.add('is-show');
    };
}, false);
