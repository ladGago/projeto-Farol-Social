@model List < ProjetoModel >

    @{
        Layout = "_LayoutDeslogado";
        ViewData["Title"] = "Mapa de Projetos";
    }

    < div class="container py-5" >
    <h1 class="text-center fw-bold text-success mb-4">Mapa de Projetos</h1>

    <div class="mb-3">
        <label for="enderecoBusca" class="form-label">Digite seu endereço:</label>
        <input type="text" id="enderecoBusca" class="form-control" placeholder="Ex: Rua X, Bairro Y, Cidade Z" />
        <button id="buscarEndereco" class="btn btn-success mt-2">Buscar</button>
    </div>

    <div id="map" style="height: 600px; width: 100%;"></div>
</div >

    @section Scripts {
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyChRXeB7_NJHyqKTstjWM8QPZ-YMrfgOQE&libraries=places"></script>
    <script>
        const projetos = @Html.Raw(System.Text.Json.JsonSerializer.Serialize(Model));

        let map;
        let geocoder;
        let userMarker;

        function initMap() {
            map = new google.maps.Map(document.getElementById("map"), {
                zoom: 12,
                center: { lat: -15.7801, lng: -47.9292 } // centro inicial
            });

            geocoder = new google.maps.Geocoder();

            // Geocodifica cada projeto e adiciona marcador
            projetos.forEach(p => {
                const enderecoCompleto = `${p.Rua}, ${p.Bairro}, ${p.Cidade}, ${p.Estado}, ${p.Cep}`;
                geocoder.geocode({ address: enderecoCompleto }, (results, status) => {
                    if (status === "OK") {
                        const pos = results[0].geometry.location;

                        const marker = new google.maps.Marker({
                            position: pos,
                            map: map,
                            title: p.NomeProjeto
                        });

                        const infoWindow = new google.maps.InfoWindow({
                            content: `
                                <h5>${p.NomeProjeto}</h5>
                                <p><strong>Tipo:</strong> ${p.Tipo}</p>
                                <p><strong>Público:</strong> ${p.Publico}</p>
                                <p><strong>Endereço:</strong> ${p.Rua}, ${p.Numero} - ${p.Bairro}, ${p.Cidade}/${p.Estado}</p>
                                <p><strong>CEP:</strong> ${p.Cep}</p>
                                <p><strong>Telefone:</strong> ${p.Telefone}</p>
                            `
                        });

                        marker.addListener("click", () => infoWindow.open(map, marker));
                    } else {
                        console.error("Erro ao geocodificar endereço:", enderecoCompleto, status);
                    }
                });
            });
        }

        // Busca do endereço do usuário
        document.getElementById("buscarEndereco").addEventListener("click", () => {
            const endereco = document.getElementById("enderecoBusca").value;
            if (!endereco) return alert("Digite um endereço!");

            geocoder.geocode({ address: endereco }, (results, status) => {
                if (status === "OK") {
                    const pos = results[0].geometry.location;

                    if (userMarker) userMarker.setMap(null);

                    userMarker = new google.maps.Marker({
                        map: map,
                        position: pos,
                        icon: "http://maps.google.com/mapfiles/ms/icons/blue-dot.png",
                        title: "Seu Endereço"
                    });

                    map.setCenter(pos);
                    map.setZoom(14);
                } else {
                    alert("Endereço não encontrado: " + status);
                }
            });
        });

        window.onload = initMap;
    </script>
}
