using SW_MES_API.DTO.Admin.Lots;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.LotRepository
{
    public interface ILotRepository
    {
        /*
        interface 를 사용하는 이유
        1. 의존성 역전(DIP) - SOLID 원칙 중 하나로, 고수준 모듈이 저수준 모듈에 의존하지 않도록 합니다.
        2. 테스트 용이성 - Mocking을 통해 인터페이스를 구현한 가짜 객체를 사용하여 단위 테스트를 쉽게 작성할 수 있습니다.
        3. 코드의 유연성 - 인터페이스를 사용하면 구현체를 쉽게 교체할 수 있어, 코드의 유연성과 확장성을 높일 수 있습니다.
        4. 코드의 가독성 - 인터페이스를 사용하면 코드의 의도를 명확하게 표현할 수 있어, 가독성이 향상됩니다.
        5. 다형성 - 인터페이스를 구현한 여러 클래스가 동일한 메서드를 제공할 수 있어, 다형성을 활용할 수 있습니다.
        6. 유지보수성 - 인터페이스를 사용하면 코드의 변경이 용이해져, 유지보수성이 향상됩니다.
        7. 코드 재사용성 - 인터페이스를 통해 공통된 기능을 정의하고, 이를 여러 클래스에서 재사용할 수 있습니다.
        8. SOLID 원칙 준수 - 인터페이스를 사용하면 SOLID 원칙을 준수할 수 있어, 객체 지향 프로그래밍의 장점을 극대화할 수 있습니다.
        */

        // 생성된 Lot을 데이터베이스에 저장하는 메서드
        Task AddLotsAsync(List<Lot> lots);

        Task UpdateLotAsync(string lotCode, int quantity);

        Task DeleteLotAsync(string lotCode);

    }
}
